using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project2.Services;
using System.Text.Json;

namespace Chat.Pages
{
    public class IndexModel : PageModel
    {
        private UserService _userService;
        private ChatService _chatService;
        private MessageService _messageService;

        public IndexModel(Chat.DAL.AppDbContext context)
        {
            _userService = new UserService(context);
            _chatService = new ChatService(context);
            _messageService = new MessageService(context);
        }

        public IActionResult OnPostAllChats(Dictionary<String, String> val)
        {
            if (val==null)
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r["status"] = "500";

                return new JsonResult(r);
            }
            string token = val["token"];

            User? me = _userService.GetUserByToken(token);

            if (me==null)
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r["status"] = "500";
                
                return new JsonResult(r);
            }

            List<ChatModel> chats =  _chatService.GetChatModels(me.Id.ToString());

            var partners = new Dictionary<int, string?>();

            for (int i = 0; i < chats.Count; i++)
            {
                ChatModel c = chats[i];
                var ids = c.UserIDs.Substring(1, c.UserIDs.Length - 2).Split("-");

                var partnerId = ids.FirstOrDefault(val =>  val != me.Id.ToString());
                if (partnerId==null)
                {
                    partners.Add(i, null);
                }
                else
                {
                    User p = _userService.GetUserById(int.Parse(partnerId));
                    partners.Add(i, JsonSerializer.Serialize(p));
                }
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result["status"] = "200";
            result["me"] = JsonSerializer.Serialize(me);
            result["chats"] = JsonSerializer.Serialize(chats);
            result["partners"] = JsonSerializer.Serialize(partners);

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        public IActionResult OnGetGetAllUsers()
        {
            List<User> users = _userService.GetAllUsers();

            users = users.OrderBy(user => user.LastOnlineAt).ToList();

            Dictionary<string, string> result = new Dictionary<string, string>();
            result["status"] = "200";
            result["users"] = JsonSerializer.Serialize(users);

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        public IActionResult OnPostSendMessage(Dictionary<String, String> val)
        {
            string id = val["id"];
            string chatID = val["chatID"];
            string message = val["message"];

            MessageModel model = new MessageModel();

            model.Message = message;
            model.SenderId = int.Parse(id);
            model.ChatId = int.Parse(chatID);
            model.CreatedAt = DateTime.Now;

            _messageService.AddMessage(model);

            Dictionary<string, string> result = new Dictionary<string, string>();
            result["status"] = "200";
            result["message"] = JsonSerializer.Serialize(model);

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        public IActionResult OnPostGetMessages(Dictionary<String, String> val)
        {
            string chatID = val["chatID"];

            List<MessageModel> messages = _messageService.GetMessages(int.Parse(chatID));


            Dictionary<string, string> result = new Dictionary<string, string>();
            result["status"] = "200";
            result["messages"] = JsonSerializer.Serialize(messages);


            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }



    }
}
