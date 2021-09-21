using AuctionApi.Common.Utils;
using AuctionApi.Domain.Contracts;
using AuctionApi.Domain.Mutations;
using AuctionApi.Domain.Queries;
using AuctionApi.Domain.Services;
using AuctionApi.Domain.Types;
using AuctionAPI.Common.Auth;
using AuctionAPI.Common.Contracts;
using AuctionAPI.Common.Mongo;
using AuctionAPI.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuctionAPI
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
            
            services.AddSingleton<IPasswordStorage, PasswordStorage>();

            services.AddScoped<IAuthenticationServices, AuthenticationServices>();

            services.AddAuthorization();

            services.AddGraphQLServer().AddAuthorization();

            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AuthenticationMutations>()
                .AddType<UserType>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            }); 

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Could not find anything");
            });
        }
    }
}
