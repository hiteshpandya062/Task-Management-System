namespace TaskManagementAPI.SharedResponses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
        public ApiResponse()
        {
            Errors = new List<string>();
        }
    }
}
