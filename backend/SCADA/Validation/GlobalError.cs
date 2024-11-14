namespace SCADA.Validation
{
    public class GlobalError
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public Dictionary<string, List<string>> errors { get; set; } = new();

        public GlobalError(int statusCode, string errorFieldName, string errorMessage)
        {
            type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            title = "One or more errors occurred.";
            status = statusCode;
            traceId = "x";
            errors.Add(errorFieldName, new List<string>{errorMessage});
        }
    }
}
