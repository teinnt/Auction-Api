using AuctionAPI.Common.Models;
using AuctionAPI.Common.Utils;
using AuctionAPI.Domain.Contracts;
using AuctionAPI.Domain.Models.Authentication;
using AuctionAPI.Routes.Types;
using AuctionAPI.Common.Auth;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionAPI.Domain.Services
{
    public class UserAuthenticationServices : IUserAuthenticationServices
    {
        private IRepository<User> _userRepository;
        private IPasswordStorage _encryptPassword;
        private IJwtHandler _jwtHandler;
        private AuthHelpers _authHelpers;

        public UserAuthenticationServices(IRepository<User> userRepository,
                                    IPasswordStorage encryptPassword,
                                    IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encryptPassword = encryptPassword;
            _jwtHandler = jwtHandler;
            _authHelpers = AuthHelpers.getAuthHelper(_userRepository, null, _encryptPassword);
        }

        public async Task<Response<JsonWebToken>> LoginUser(LoginInput input)
        {
            Response<JsonWebToken> response = new Response<JsonWebToken>();
            string email = input.Email.Trim();
            string password = input.Password.Trim();

            string errorMessage = await _authHelpers.VerifyLogin(email, password, UserRole.User);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.Error = new ErrorModel { Message = errorMessage, Code = "UNAUTHORIZED" };
                return response;
            }

            try
            {
                var user = (await _userRepository.Get(x => x.Email == email)).First();
                var jsonWebToken = _jwtHandler.Create(user.Id, UserRole.User);
                jsonWebToken.UserName = user.UserName.Trim();

                response.Data = jsonWebToken;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorModel { Message = "Login error - " + ex.Message, Code = "UNAUTHORIZED" };
                return response;
            }
        }

        public async Task<Response<JsonWebToken>> RegisterUser(AddUserInput input)
        {
            Response<JsonWebToken> response = new Response<JsonWebToken>();
            string userName = input.UserName.Trim();
            string email = input.Email.Trim();
            string password = input.Password.Trim();

            string errorMessage = await _authHelpers.VerifyRegisterUser(userName, email, password);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.Error = new ErrorModel { Message = errorMessage, Code = "UNAUTHORIZED" };
                return response;
            }

            try
            {
                var user = new User()
                {
                    UserRole = UserRole.User,
                    UserName = userName,
                    Email = email,
                    PasswordHash = _encryptPassword.CreateHash(password),
                };

                await _userRepository.Add(user);
                var jsonWebToken = _jwtHandler.Create(user.Id, user.UserRole);
                jsonWebToken.UserName = user.UserName;

                response.Data = jsonWebToken;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorModel { Message = "Register error - " + ex.Message, Code = "UNAUTHORIZED" };
                return response;
            }
        }
    }
}
