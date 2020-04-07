using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PollContext.Domain.Handlers;
using PollContext.Domain.Repositories;
using PollContext.Domain.Services;
using PollContext.Infra.Contexts;
using PollContext.Infra.Repositories;
using PollContext.Infra.Services;
using PollContext.Shared;
using System.IO;
using System.Linq;
using System.Text;

namespace PollContext.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            // services.AddResponseCaching();
            services.AddControllers();
            var config = Configuration.GetConnectionString("connectionString");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(config));
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Poll"));

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<IOptionPollRepository, OptionPollRepository>();
            services.AddTransient<PollHandler, PollHandler>();
            services.AddTransient<OptionPollHandler, OptionPollHandler>();

            //autenticação local
            #region autenticação local 
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
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
            #region autenticação google firebase 
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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Enquete Api", Version = "v1" });
            });

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
            //elmah log web.
            //services.AddElmahIo(o =>
            //{
            //    o.ApiKey = "48cafea287cf4b6da4fd614c555deb15";
            //    o.LogId = new Guid("24bd0a4a-7158-4bad-bae9-acd03c2a3a56");
            //});
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enquete V1");
            });

            //log com elmah
            //app.UseElmahIo();

        }
    }
}
