using LibraryServicesAPI.DBContext;
using LibraryServicesAPI.DTO;
using LibraryServicesAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace LibraryServicesAPI.ServiceLayer
{
    public interface IUser
    {
        public bool CreateUserAccount(User user);
        public string CreateHashedPassword(string password);
        public bool IsuserExists(string username, string password);
        public bool IsBothPasswordsAreValid(string newpassword, string reenternewpassword);
        public bool ResetPassword(ResetPassword resetPassword);
    }
    public class UserService : IUser
    {
        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool CreateUserAccount(User user)
        {
            string userpassword = CreateHashedPassword(user.Password);
            if (IsuserExists(user.UserName, userpassword))
            {
                return false;
            }
            user.Password = userpassword;
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return true;
        }
        public string CreateHashedPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (var h in hash)
            {
                sb.Append(h.ToString("x2"));
            }
            return sb.ToString();
        }
        public bool IsuserExists(string username, string password)
        {
            return _appDbContext.Users.Any
                (x => x.UserName == username &&
                 x.Password == password);
        }
        public bool IsBothPasswordsAreValid(string newpassword, string reenternewpassword)
        {
            if (newpassword == reenternewpassword)
            {
                return true;
            }
            return false;
        }
        public bool ResetPassword(ResetPassword resetPassword)
        {
            string newhashedpass = CreateHashedPassword(resetPassword.NewPassword);
            var user = _appDbContext.Users.FirstOrDefault(x => x.UserName == resetPassword.Username);
            if(user != null)
            {
                user.Password = newhashedpass;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
