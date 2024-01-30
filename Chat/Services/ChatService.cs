using Chat.DAL;
using Chat.Models;

namespace Chat.Services
{
    public class ChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public ChatModel? GetChat(string ids) {
            return _context.Chats.FirstOrDefault(c => c.UserIDs == ids);
        }

        public ChatModel CreateChat(ChatModel chat) { 
            _context.Chats.Add(chat);

            _Save();

            return GetChat(chat.UserIDs)!;
        }

        public List<ChatModel> GetChatModels(string id)
        {
            string newId = "-"+id+"-";
            return _context.Chats.OrderBy(c => c.UserIDs.Contains(newId)).ToList();
        }

        private void _Save()
        {
            _context.SaveChanges();
        }
    }
}
