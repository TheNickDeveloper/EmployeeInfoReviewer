using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Services;
using EmployeeInfoReviewer.Services.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace EmployeeInfoReviewer
{
    public enum DbOptions
    {
        SqlServer,
        Sqlite,
        MongoDb
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public DbOptions TargetDbName { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            TargetDbName = DbOptions.SqlServer;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var clientDomain = Configuration.GetValue<string>("ClientDomain");

            // enable to build the connection from local db for http request
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                b =>
                {
                    b.WithOrigins(clientDomain)
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowAnyOrigin();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            InitializeLogger();

            #region DotNet self DI
            //services.AddDbContext<PeopleContext>(options =>
            //{
            //    //Sqlite use
            //    options.UseSqlite(Configuration.GetConnectionString("Sqlite"));

            //    ////SqlServer use
            //    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            //});

            //MongoDb use
            //services.AddSingleton<IConfiguration>(Configuration);
            #endregion

            #region Autofac DI

            // auto mapper DI
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();

            var builder = new ContainerBuilder();
            InJectDbContext(builder, mapper);
            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
            #endregion
        }
        private void InitializeLogger()
        {
            // Build Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(Configuration.GetSection("Logging").GetValue<string>("Path")
                    + $"EmployeeInfoReviewer-{DateTime.UtcNow.Date.ToString("yyyyMMdd")}.txt")
                .CreateLogger();
        }

        private void InJectDbContext(ContainerBuilder builder, IMapper mapper)
        {
            // dbContext DI
            switch (TargetDbName)
            {
                case DbOptions.Sqlite:
                case DbOptions.SqlServer:
                    builder.Register(ctx =>
                    {
                        var optionBuilder = new DbContextOptionsBuilder<PeopleContext>();
                        if (TargetDbName == DbOptions.Sqlite)
                        {
                            optionBuilder.UseSqlite(Configuration.GetConnectionString(Enum.GetName(typeof(DbOptions), TargetDbName)));
                        }

                        if (TargetDbName == DbOptions.SqlServer)
                        {
                            optionBuilder.UseSqlServer(Configuration.GetConnectionString(Enum.GetName(typeof(DbOptions), TargetDbName)));
                        }

                        var peopleContext = new PeopleContext(optionBuilder.Options);

                        return new PeopleService(peopleContext, mapper);
                    }).As<IPeopleService>();
                    break;

                case DbOptions.MongoDb:
                    builder.Register(ctx =>
                    {
                        return new MongoDbPeopleService(Configuration, mapper);
                    }).As<IPeopleService>();
                    break;
            }

            Log.Information($"Startup: Use {Enum.GetName(typeof(DbOptions), TargetDbName)} as DB. [{DateTime.UtcNow}]");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
