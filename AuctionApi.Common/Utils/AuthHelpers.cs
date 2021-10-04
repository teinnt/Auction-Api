using AuctionAPI.Common.Models;
using AuctionAPI.Common.Auth;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using System.Threading.Tasks;
using System.Linq;

namespace AuctionAPI.Common.Utils
{
    public class AuthHelpers
    {
        public static AuthHelpers Instances;

        private static IRepository<Company> _companyRepository { get; set; }
        private static IRepository<User> _userRepository { get; set; }
        private static IPasswordStorage _encryptPassword { get; set; }

        private AuthHelpers() { }

        public static AuthHelpers getAuthHelper(IRepository<User> userRepo,
            IRepository<Company> companyRepo,
            IPasswordStorage passwordStorage)
        {
            if (Instances == null)
            {
                Instances = new AuthHelpers();
                _encryptPassword = passwordStorage;
            }

            if (userRepo != null && _userRepository == null)
            {
                _userRepository = userRepo;
            }

            if (companyRepo != null && _companyRepository == null)
            {
                _companyRepository = companyRepo;
            }
            
            return Instances;
        }

        public async Task<string> VerifyLogin(string email, string password, UserRole userRole)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "The email is empty!";
            }

            if (string.IsNullOrEmpty(password))
            {
                return "The password is empty!";
            }

            string passwordHash = "";

            if (userRole == UserRole.User)
            {
                User user = (await _userRepository.Get(x => x.Email == email)).FirstOrDefault();
                if (user == null)
                {
                    return "The email does not exist!";
                }

                passwordHash = user.PasswordHash;
            }
            else
            {
                Company user = (await _companyRepository.Get(x => x.Email == email)).FirstOrDefault();
                if (user == null)
                {
                    return "The email does not exist!";
                }

                passwordHash = user.PasswordHash;
            }

            if (!_encryptPassword.VerifyPassword(password, passwordHash))
            {
                return "Invalid credentials!";
            }

            return "";
        }

        public async Task<string> VerifyRegisterUser(string userName, string email, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return "User name is empty!";
            }

            if (string.IsNullOrEmpty(email))
            {
                return "The email is empty!";
            }

            if (string.IsNullOrEmpty(password))
            {
                return "The password is empty!";
            }

            User user = (await _userRepository.Get(x => x.Email == email)).FirstOrDefault();
            if (user != null)
            {
                return "The email already exists!";
            }

            return "";
        }

        // TODO: Verify company register
        public async Task<string> VerifyRegisterCompany(string companyName, string email, string password)
        {
            return "";
        }
    }
}
