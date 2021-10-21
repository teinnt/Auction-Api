using AuctionApi.Common.Models;
using AuctionApi.Common.Utils;
using AuctionApi.Domain.Contracts;
using AuctionApi.Domain.Models.Authentication;
using AuctionApi.Routes.Types;
using AuctionApi.Common.Auth;
using AuctionApi.Common.Contracts;
using AuctionApi.Common.Models;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApi.Domain.Services
{
    public class CompanyAuthenticationServices : ICompanyAuthenticationServices
    {
        private IRepository<Company> _companyRepository;
        private IPasswordStorage _encryptPassword;
        private IJwtHandler _jwtHandler;
        private AuthHelpers _authHelpers;

        public CompanyAuthenticationServices(IRepository<User> userRepository,
                                    IRepository<Company> companyRepository,
                                    IPasswordStorage encryptPassword,
                                    IJwtHandler jwtHandler)
        {
            _companyRepository = companyRepository;
            _encryptPassword = encryptPassword;
            _jwtHandler = jwtHandler;
            _authHelpers = AuthHelpers.getAuthHelper(null, _companyRepository, _encryptPassword);
        }

        public async Task<Response<JsonWebToken>> LoginCompany(LoginInput input)
        {
            Response<JsonWebToken> response = new Response<JsonWebToken>();
            string email = input.Email.Trim();
            string password = input.Password.Trim();

            string errorMessage = await _authHelpers.VerifyLogin(email, password, UserRole.AuctionHost);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.Error = new ErrorModel { Message = errorMessage, Code = "UNAUTHORIZED" };
                return response;
            }

            try
            {
                var company = (await _companyRepository.Get(x => x.Email == email)).First();
                var jsonWebToken = _jwtHandler.Create(company.Id, UserRole.User);
                jsonWebToken.UserName = company.CompanyName;

                response.Data = jsonWebToken;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorModel { Message = "Login error - " + ex.Message, Code = "UNAUTHORIZED" };
                return response;
            }
        }

        public async Task<Response<JsonWebToken>> RegisterCompany(AddCompanyInput input)
        {
            Response<JsonWebToken> response = new Response<JsonWebToken>();
            string companyName = input.CompanyName.Trim();
            string email = input.Email.Trim();
            string password = input.Password.Trim();

            string errorMessage = await _authHelpers.VerifyRegisterCompany(companyName, email, password);
            if (string.IsNullOrEmpty(errorMessage))
            {
                response.Error = new ErrorModel { Message = errorMessage, Code = "UNAUTHORIZED" };
                return response;
            }

            try
            {
                var company = new Company()
                {
                    Email = input.Email,
                    PasswordHash = _encryptPassword.CreateHash(input.Password),
                    CompanyName = input.CompanyName,
                    ISIN = input.Isin,
                    Address = new Address 
                    { 
                        HouseNumber = input.HouseNumber, 
                        City = input.City, 
                        State = input.State,  
                        StreetAddress = input.StreetAddress,
                        ZipCode = input.ZipCode,
                        Country = input.Country
                    },
                    WalletAddress = input.WalletAddress,
                    ContactNumber = input.ContactNumber,
                    LaunchOn = input.LaunchOn,
                    RepresentativeName = input.RepresentativeName,
                    RepresentativeEmail = input.RepresentativeEmail,
                    RepresentativeIdUrl = input.RepresentativeIdUrl,
                    RepresentativePhoneNumber = input.RepresentativePhoneNumber,
                };

                await _companyRepository.Add(company);
                var jsonWebToken = _jwtHandler.Create(company.Id, UserRole.AuctionHost);
                jsonWebToken.UserName = company.CompanyName;

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
