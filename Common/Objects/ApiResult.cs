namespace Common.Objects
{
    public class ApiResult<T>
    {
        public bool IsSuccessful { get; set; }
        public required T Value { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
