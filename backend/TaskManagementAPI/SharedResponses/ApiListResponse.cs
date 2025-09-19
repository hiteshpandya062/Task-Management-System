namespace TaskManagementAPI.SharedResponses
{
    public class ApiListResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ApiListResponse()
        {
            Errors = new List<string>();
        }
    }
}
