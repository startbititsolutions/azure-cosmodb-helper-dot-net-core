namespace Cosmos_console.Model
{
    public class Message
    {
        public string id { get; set; }
        public string type { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string userId { get; set; }
        public string conversationId { get; set; }
        public string role { get; set; }
        public string content { get; set; }
        public string? feedback{ get; set; }
    }
}
