using Chat.DAL;
using Chat.Models;

namespace Project2.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void SetUser(User user)
        {
            _context.Users.Add(user);
            _Save();
        }

        public User? GetUserById(int id) { 
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User? GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void SetProfilePicture(string token, byte[] file)
        {
            User? user = GetUserByToken(token);
            if (user != null) return;

            user!.ProfileImage = file;

            _Save();
        }

        public User? GetUserByToken(string token)
        {
            return _context.Users.FirstOrDefault(u => u.Token == token);
        }

            private void _Save()
            {
                _context.SaveChanges();
            }
    }
}