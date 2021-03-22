using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PRO.API.Extensions;
using PRO.API.Settings;
using PRO.Core;
using PRO.Core.Models.Auth;
using PRO.Core.Services;
using PRO.Data;
using PRO.Services;
using PRO.Services.SignalR;

namespace PRO.API
{
    public class Startup
    {
       readonly string MyAllowSpecificOrigins = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(
               Configuration.GetConnectionString("Hangfire")));
            services.AddHangfireServer();
            services.AddControllers();

            //add dependence
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<INotificationService, NotificationService>();


            //add Db context
            services.AddDbContext<ProDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("PRO.Data")));

            //fix bug json when return
            services.AddControllersWithViews()
                  .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

            //add swagger
            services.AddSwaggerGen(options =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Music", Version = "v1" });
            });

            //config auto mapper
            services.AddAutoMapper(typeof(Startup));

            //config identity 
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
            })
            .AddEntityFrameworkStores<ProDbContext>()
            .AddDefaultTokenProviders();

            //config jwt
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            //add config authorization 
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuth(jwtSettings);

            //add signalR
            services.AddSignalR();


            //add cors 
            //services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //           .AllowCredentials()
            //           .WithOrigins("http://localhost:4200");
            //}));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                       .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                                  });
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Enable Cors 
            app.UseCors(MyAllowSpecificOrigins);

            //config authorization attribute
            app.UseAuth();

            //config signalR 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/notify");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //add hangfire
            app.UseHangfireDashboard("/mydashboard");
            app.UseHangfireServer();

            // BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));

            //var jobId = BackgroundJob.Schedule(
            //    () => Console.WriteLine("Delayed!"),
            //    TimeSpan.FromMinutes(1));

            //BackgroundJob.ContinueJobWith(
            //    jobId,
            //    () => Console.WriteLine("Continuation!"));


            //using(HttpClient client = new HttpClient())
            //            {
            //                RecurringJob.AddOrUpdate(
            //               () => client.PostAsync("1231")), Cron.Minutely);
            //            }


            //Set Recurring job Hangfire
            //using (HttpClient client = new HttpClient())
            //{

            //    RecurringJob.AddOrUpdate(() => client.GetAsync("http://localhost:5000/api/Music/unsubscribe"), Cron.Minutely);
            //}

            //config swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
            });
        }
    }
}
