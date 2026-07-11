using System.Text.Json.Serialization;

namespace FinanceCore.API.Models
{
    public class StandardProblemDetails
    {
        [JsonPropertyName("traceId")]
        public string? TraceId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = ErrorCodes.Blanc;
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("detail")]
        public string? Detail {  get; set; }
        [JsonPropertyName("instance")]
        public string? Instance { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [JsonPropertyName("errors")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string[]>? Errors { get; set; }

        public StandardProblemDetails() { }
        public StandardProblemDetails(int statusCode, string title, string? detail = null)
        {
            Status = statusCode;
            Title = title;
            Detail = detail;
        }
    }
}
