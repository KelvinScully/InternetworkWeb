namespace Common
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool IsLocal { get; set; }
        public bool IsIISExpress { get; set; }
    }
}
