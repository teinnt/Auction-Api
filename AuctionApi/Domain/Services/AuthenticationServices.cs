using AuctionApi.Common.Models;
using AuctionApi.Domain.Contracts;
using AuctionApi.Domain.Models.Authentication;
using AuctionAPI.Common.Auth;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private IRepository<Company> _companyRepository;
        private IRepository<User> _userRepository;
        private IPasswordStorage _encryptPassword;
        private IJwtHandler _jwtHandler;

        public AuthenticationServices(IRepository<User> userRepository,
                                    IRepository<Company> companyRepository,
                                    IPasswordStorage encryptPassword,
                                    IJwtHandler jwtHandler)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _encryptPassword = encryptPassword;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginCompany(string email, string password)
        {
            if (await IsEmailExisted(email, UserRole.AuctionHost) == false)
            {
                throw new ApplicationException("The email has not been registered!");
            }

            if (await VerifyPassword(email, password, UserRole.AuctionHost) == false)
            {
                throw new ApplicationException("Invalid credentials");
            }

            try
            {
                var company = (await _companyRepository.Get(x => x.Email == email)).First();

                var jsonWebToken = _jwtHandler.Create(company.Id, UserRole.User);

                jsonWebToken.UserName = company.CompanyName;

                return jsonWebToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        public async Task<JsonWebToken> LoginUser(string email, string password)
        {
            if (await IsEmailExisted(email, UserRole.User) == false)
            {
                throw new ApplicationException("The email has not been registered!");
            }

            if (await VerifyPassword(email, password, UserRole.User) == false)
            {
                throw new ApplicationException("Invalid credentials");
            }

            try
            {
                var user = (await _userRepository.Get(x => x.Email == email)).First();

                var jsonWebToken = _jwtHandler.Create(user.Id, UserRole.User);

                jsonWebToken.UserName = user.UserName;

                return jsonWebToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        public async Task<JsonWebToken> RegisterCompany(AddCompanyInput input)
        {
            if (await IsEmailExisted(input.Email, UserRole.AuctionHost))
            {
                throw new ApplicationException("The email was already registered!");
            }

            try
            {
                var company = new Company
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    UId = Guid.NewGuid(),
                    Email = input.Email,
                    PasswordHash = _encryptPassword.CreateHash(input.Password),
                    CompanyName = input.CompanyName,
                    ISIN = input.ISIN,
                    Address = new Address 
                    { 
                        HouseNumber = input.HouseNumber, 
                        City = input.City, 
                        State = input.State,  
                        StreetAddress = input.StreetAddress,
                        ZipCode = input.ZipCode
                    },
                    WalletAddress = input.WalletAddress,
                    ContactNumber = input.ContactNumber,
                    LaunchOn = input.LaunchOn,
                    RepresentativeName = input.RepresentativeName,
                    RepresentativeEmail = input.RepresentativeEmail,
                    RepresentativeIdUrl = input.RepresentativeIdUrl,
                    RepresentativePhoneNumber = input.RepresentativePhoneNumber,
                    IsDeleted = false
                };

                await _companyRepository.Add(company);

                var jsonWebToken = _jwtHandler.Create(company.Id, UserRole.AuctionHost);

                jsonWebToken.UserName = company.CompanyName;

                return jsonWebToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        public async Task<JsonWebToken> RegisterUser(AddUserInput input)
        {
            if (await IsEmailExisted(input.Email, UserRole.User))
            {
                throw new ApplicationException("The email was already registered!");
            }

            try
            {
                var user = new User
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    UId = Guid.NewGuid(),
                    UserRole = UserRole.User,
                    UserName = input.UserName,
                    Email = input.Email,
                    CreatedOn = DateTime.UtcNow,
                    BoughtItemIds = new List<string>(),
                    SoldItemIds = new List<string>(),
                    PasswordHash = _encryptPassword.CreateHash(input.Password),
                    IsDeleted = false
                };

                await _userRepository.Add(user);

                var jsonWebToken = _jwtHandler.Create(user.Id, user.UserRole);
                jsonWebToken.UserName = user.UserName;

                return jsonWebToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        #region Helpers

        public async Task<bool> IsEmailExisted(string email, UserRole userRole)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("Fail to validate - Email is empty");
            }

            if (userRole == UserRole.User)
            {
                return (await _userRepository.Get(x => x.Email == email)).FirstOrDefault() != null;
            }
            else
            {
                return (await _companyRepository.Get(x => x.Email == email)).FirstOrDefault() != null;
            }
        }

        public async Task<bool> VerifyPassword(string email, string password, UserRole userRole)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ApplicationException("Login fail - Password is empty");
            }

            if (userRole == UserRole.User)
            {
                var user = (await _userRepository.Get(x => x.Email == email)).FirstOrDefault();

                if (user != null)
                {
                    return _encryptPassword.VerifyPassword(password, user.PasswordHash);
                }
            }
            else
            {
                var company = (await _companyRepository.Get(x => x.Email == email)).FirstOrDefault();

                if (company != null)
                {
                    return _encryptPassword.VerifyPassword(password, company.PasswordHash);
                }
            }

            return false;
        }

        #endregion
    }
}
