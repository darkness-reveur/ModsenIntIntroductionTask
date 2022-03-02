using MeetupPlatform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace VladosMeetupPlatform.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {   
                        builder
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .WithOrigins("https://localhost:5001")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddDbContext<MeetupPlatformContext>(options =>
                options.UseNpgsql(Configuration
                    .GetSection("ConnectionStrings")
                        .GetValue<string>("DefaultDbConnection")));

            services.RegisterDependesy();

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.
                MapDefaultControllerRoute();
            });
        }
    }
}
