namespace Qinile.Core.Services
{
    public class Meta<K>
    {
        public int StatusCode { get; set; }
        public K Data { get; set; }
        public string Message { get; set; }
    }
}