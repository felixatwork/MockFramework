public class Mock
{
    public string RequestPath { get; set; }
    public string RequestPayload { get; set; }
    public string ResponsePayload { get; set; }
    public int? Delay { get; set;}
    public int? HttpStatusCode { get; set; }
}