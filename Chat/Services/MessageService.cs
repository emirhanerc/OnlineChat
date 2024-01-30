using Chat.DAL;
using Chat.Models;

namespace Chat.Services
{
    public class MessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        public List<MessageModel> GetMessages(int chatID)
        {
            return _context.Messages.Where(m => m.ChatId == chatID).ToList();
        }

        public void AddMessage(MessageModel message)
        {
            _context.Messages.Add(message);
            
            _Save();
        }

        private void _Save()
        {
            _context.SaveChanges();
        }
    }
}
