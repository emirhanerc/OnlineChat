namespace Chat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public byte[]? ProfileImage { get; set; }
        public int IsAdmin { get; set; }
        public DateTime LastOnlineAt { get; set; }
        public string PictureID { get; set; }
    }
}
