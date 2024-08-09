namespace MessageApi.Models
{
    public class MessageDto
    {
        public string Text { get; set; }
        public long Timestamp { get; set; }  // Add this line to include the timestamp
    }


}
