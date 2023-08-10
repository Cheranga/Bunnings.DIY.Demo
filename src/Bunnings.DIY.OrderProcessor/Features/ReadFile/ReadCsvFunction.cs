using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Bunnings.DIY.OrderProcessor.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Bunnings.DIY.OrderProcessor.Features.ReadFile;

public class ReadCsvFunction
{
    private readonly ILogger<ReadCsvFunction> _logger;
    private readonly ReadFileConfig _fileConfig;

    public ReadCsvFunction(ReadFileConfig config, ILogger<ReadCsvFunction> logger)
    {
        _logger = logger;
        _fileConfig = config;
    }

    [FunctionName(nameof(ReadCsvFunction))]
    public async Task RunAsync(
        [BlobTrigger("%ReadFileConfig:CsvPath%/{name}")]
            TextReader textReader,
        string name,
        [Queue("%ReadFileConfig:Queue%")]
            QueueClient queueClient
    )
    {
        _logger.LogInformation("Triggered for {@FileName}", name);

        var fileContent = await textReader.ReadToEndAsync();
        var fileLines = fileContent.Split("\n");

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var tasks = (_fileConfig.ContainsHeader ? fileLines.Skip(1) : fileLines).Aggregate(
            new List<Task>(),
            (list, s) =>
            {
                list.Add(
                    queueClient.SendMessageAsync(
                        BinaryData.FromString(s),
                        timeToLive: _fileConfig.TimeToLiveInSeconds.ToTimeSpan()
                    )
                );
                return list;
            }
        );

        await Task.WhenAll(tasks);

        stopWatch.Stop();

        _logger.LogInformation(
            "{FileName} was read in {TimeTaken} ms",
            name,
            stopWatch.ElapsedMilliseconds
        );
    }
}
