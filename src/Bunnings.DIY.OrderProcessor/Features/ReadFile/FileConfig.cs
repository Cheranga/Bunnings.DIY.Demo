namespace Bunnings.DIY.OrderProcessor.Features.ReadFile;

public record ReadFileConfig
{
    public string CsvPath { get; set; }
    public string Queue { get; set; }
    public bool ContainsHeader { get; set; } = true;
    public int TimeToLiveInSeconds { get; set; } = 30;
    public string Connection { get; set; }
}
