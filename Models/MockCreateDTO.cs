namespace MockFramework.Models;

public class MockCreateDto {

    public string RequestPath { get; set; }

    public string RequestPayload { get; set; }

    public string ResponsePayload { get; set; }

    public int DelayTime { get; set; }

    public int HttpStatusCode { get; set; }
}