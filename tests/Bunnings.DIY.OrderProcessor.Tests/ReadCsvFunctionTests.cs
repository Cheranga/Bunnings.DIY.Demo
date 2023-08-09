using System.Net;
using Azure;
using Azure.Core.Pipeline;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Bunnings.DIY.OrderProcessor.Features.ReadFile;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bunnings.DIY.OrderProcessor.Tests;

public static class ReadCsvFunctionTests
{
    [Fact(DisplayName = "File with content must publish messages to queue")]
    public static async Task FileWithContentMustPublishToQueue()
    {
        var queueClient = new Mock<QueueClient>();
        queueClient
            .Setup(
                x =>
                    x.SendMessageAsync(
                        It.IsAny<BinaryData>(),
                        null,
                        It.IsAny<TimeSpan>(),
                        It.IsAny<CancellationToken>()
                    // It.IsAny<TimeSpan>(),
                    // It.IsAny<TimeSpan>(),
                    // It.IsAny<CancellationToken>()
                    )
            )
            .ReturnsAsync(
                Response.FromValue(
                    QueuesModelFactory.SendReceipt(
                        "1",
                        DateTimeOffset.Now,
                        DateTimeOffset.Now.AddSeconds(10),
                        "receipt1",
                        DateTimeOffset.UtcNow.AddSeconds(60)
                    ),
                    DummyResponse.SuccessResponse(HttpStatusCode.OK)
                )
            );

        await new ReadCsvFunction(
            new ReadFileConfig
            {
                Connection = "connectionstring",
                Queue = "queue",
                Separator = "\n",
                ContainsHeader = true,
                CsvPath = "pathtocsv",
                TimeToLiveInSeconds = 30
            },
            Mock.Of<ILogger<ReadCsvFunction>>()
        ).RunAsync(
            new StringReader("header1, header2, header3\n" + "1, 2, 3"),
            "filename",
            queueClient.Object
        );

        queueClient.Verify(
            client =>
                client.SendMessageAsync(
                    It.IsAny<BinaryData>(),
                    null,
                    It.IsAny<TimeSpan>(),
                    It.IsAny<CancellationToken>()
                ),
            Times.Exactly(1)
        );
    }
}
