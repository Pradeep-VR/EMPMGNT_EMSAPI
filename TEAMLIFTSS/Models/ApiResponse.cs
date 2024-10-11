namespace TEAMLIFTSS.Models
{
    public class ApiResponse
    {
        public int ResponseCode { get; set; }
        public string Result { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }

    public class Api
    {
        public string? result { get; set; }
        public dynamic? data { get; set; }
    }
}
