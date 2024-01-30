namespace Chat.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public int SenderId { get; set; }
        public int ChatId { get; set; }


    }
}
