using AuctionApi.Common.Utils;
using AuctionApi.Domain.Contracts;
using AuctionApi.Domain.Mutations;
using AuctionApi.Domain.Queries;
using AuctionApi.Domain.Services;
using AuctionApi.Routes.Mutations;
using AuctionApi.Routes.Types;
using AuctionApi.Common.Auth;
using AuctionApi.Common.Contracts;
using AuctionApi.Common.Mongo;
using AuctionApi.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuctionApi.Common.Security;
using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace AuctionApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; } = default!;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(Constants.corsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddJwt(Configuration);

            services.AddMongoDB(Configuration);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Func<IServiceProvider, IPrincipal> getPrincipal =
                     (sp) => sp.GetService<IHttpContextAccessor>().HttpContext.User;

            services.AddScoped(typeof(Func<IPrincipal>), sp =>
            {
                Func<IPrincipal> func = () =>
                {
                    return getPrincipal(sp);
                };
                return func;
            });

            services.AddScoped<IUserAppContext, UserAppContext>();
            services.AddSingleton<IPasswordStorage, PasswordStorage>();
            services.AddScoped<IUserAuthenticationServices, UserAuthenticationServices>();
            services.AddScoped<IBidAuctionServices, BidAuctionServices>();
            services.AddScoped<IAuctionServices, AuctionServices>();

            services.AddSignalR(x => x.EnableDetailedErrors = true)
                .AddAzureSignalR(Configuration["AzureSignalR:ConnectionString"]);

            services.AddAuthorization();

            services.AddGraphQLServer().AddAuthorization();

            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                    .AddTypeExtension<AuctionQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<UserAuthenticationMutations>()
                    .AddTypeExtension<CompanyAuthenticationMutations>()
                    .AddTypeExtension<AuctionMutations>()
                .AddType<UserType>()
                .AddType<CompanyType>()
                .AddType<ResponseUserType>()
                .AddType<ResponseCompanyType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            }

            app.UseCors(Constants.corsPolicy);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseAzureSignalR(route =>
            {
                route.MapHub<BidAuctionHub>("/auction/bid");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Could not find anything");
            });
        }
    }
}
