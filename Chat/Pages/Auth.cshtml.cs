using Azure;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project2.Services;
using System.Text;

namespace Chat.Pages
{
    public class AuthModel : PageModel
    {
        private UserService _userService;

        public AuthModel(Chat.DAL.AppDbContext context)
        {
            _userService = new UserService(context);
        }

        public IActionResult OnPostLogin(Dictionary<String, String> val)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string username = val["username"];
            string password = val["password"];

            User? u = _userService.GetUserByUsernameAndPassword(username, password);

            if (u==null)
            {
                result["status"] = "300";
                result["error"] = "Kullanıcı adı ve şifre yanlış";

                return new JsonResult(result);
            }


            result["token"] = u.Token;
            result["status"] = "200";

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        public IActionResult OnPostRegister(Dictionary<String, String> val)
        {
            string username = val["username"];
            string password = val["password"];


            User user = new User();
            user.Password = password;
            user.Username = username;
            user.IsAdmin = 0;
            user.PictureID = GetRandomIntAsString();
            user.LastOnlineAt = DateTime.Now;
            string token = _GenerateRandomString();
            user.Token = token;

            _userService.SetUser(user);


            Dictionary<string, string> result = new Dictionary<string, string>();

            result["token"] = user.Token;
            result["status"] = "200";

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        public IActionResult OnPostUpload(IFormFile file) {
            byte[] fileData;
            string token = "";

            if (Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValue))
            {
                token = authorizationHeaderValue.ToString();
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileData = memoryStream.ToArray();
            }

            _userService.SetProfilePicture(token,fileData);

            Dictionary<string, string> result = new Dictionary<string, string>();

            result["status"] = "200";

            //burda 200 d�nd�r�yoruz. yani OK demek
            return new JsonResult(result);
        }

        private string GetRandomIntAsString()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 9);

            return randomNumber.ToString();
        }

        private string _GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder randomStringBuilder = new StringBuilder(15);
            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                randomStringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return randomStringBuilder.ToString();
        }

    }
}
