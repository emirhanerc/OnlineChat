using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project2.Services;
using System.Text.Json;
using System;
using Chat.Models;

namespace Chat.Pages
{
    public class GroupModel : PageModel
    {
        private UserService _userService;
        private ChatService _chatService;
        private MessageService _messageService;

        public GroupModel(Chat.DAL.AppDbContext context)
        {
            _userService = new UserService(context);
            _chatService = new ChatService(context);
            _messageService = new MessageService(context);
        }

        public IActionResult OnPostCreateChat(Dictionary<String, String> val)
        {
            string ids = val["ids"];
            string? name = null;

            if (val.ContainsKey("name"))
            {
                name = val["name"];
            }

            ChatModel chatModel = new ChatModel();
            chatModel.Name = name;
            chatModel.UserIDs = ids;
            chatModel.LastMessageAt = DateTime.Now;

            _chatService.CreateChat(chatModel);


            Dictionary<string, string> result = new Dictionary<string, string>();
            result["status"] = "200";
            //burda 200 d?nd?r?yoruz. yani OK 

            return new JsonResult(result);
        }
    }
}
