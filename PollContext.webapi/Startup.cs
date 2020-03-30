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

            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<IOptionPollRepository, OptionPollRepository>();
            services.AddTransient<PollHandler, PollHandler>();
            services.AddTransient<OptionPollHandler, OptionPollHandler>();

            //autorização por meio do firebase
            #region autorização firebase
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
