using System.Net;
using Azure;
using Azure.Core;

namespace Bunnings.DIY.OrderProcessor.Tests;

public class DummyResponse : Response
{
    private DummyResponse(HttpStatusCode errorStatusCode, string reason)
    {
        Status = (int)errorStatusCode;
        ReasonPhrase = reason;
    }

    public override int Status { get; }
    public override string ReasonPhrase { get; }
    public override Stream ContentStream { get; set; }
    public override string ClientRequestId { get; set; } = Guid.NewGuid().ToString();

    public override bool IsError => !string.IsNullOrEmpty(ReasonPhrase);

    public override void Dispose() { }

    protected override bool TryGetHeader(string name, out string value)
    {
        value = "";
        return true;
    }

    protected override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
    {
        values = Array.Empty<string>();
        return true;
    }

    protected override bool ContainsHeader(string name) => true;

    protected override IEnumerable<HttpHeader> EnumerateHeaders() => Array.Empty<HttpHeader>();

    public static DummyResponse SuccessResponse(HttpStatusCode statusCode) =>
        new(statusCode, string.Empty);

    public static DummyResponse FailedResponse(HttpStatusCode errorStatusCode, string reason) =>
        new(errorStatusCode, reason);
}
