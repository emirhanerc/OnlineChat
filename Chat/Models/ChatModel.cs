namespace Chat.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public string UserIDs { get; set; }
        public string? Name { get; set; }
        public DateTime LastMessageAt { get; set; }
    }
}
