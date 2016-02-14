namespace ZendeskApi_v2.Models.Shared
{
    public class ZenFile
    {        
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}