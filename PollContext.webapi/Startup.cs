using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PollContext.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Handlers;
using PollContext.Domain.Repositories;
using PollContext.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PollContext.Infra.Settings;
using PollContext.Domain.Services;
using PollContext.Infra.Services;

namespace PollContext.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var config = Configuration.GetConnectionString("connectionString");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(config));
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<IOptionPollRepository, OptionPollRepository>();
            services.AddTransient<PollHandler, PollHandler>();
            services.AddTransient<OptionPollHandler, OptionPollHandler>();


            #region autorização 
            var key = Encoding.ASCII.GetBytes(Setting.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            //autorização por meio do firebase
            #region autorização google firebase 
            //services
            //   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       options.Authority = "https://securetoken.google.com/polls-f56df";
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuer = true,
            //           ValidIssuer = "https://securetoken.google.com/polls-f56df",
            //           ValidateAudience = true,
            //           ValidAudience = "polls-f56df",
            //           ValidateLifetime = true
            //       };
            //   });

            #endregion



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthorization();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
