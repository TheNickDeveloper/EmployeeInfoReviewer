using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeInfoReviewer
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
            var builder = new ContainerBuilder();

            switch (TargetDbName)
            {
                case DbOptions.Sqlite:
                    builder.Register(ctx =>
                    {
                        var optionBuilder = new DbContextOptionsBuilder<PeopleContext>();
                        optionBuilder.UseSqlite(Configuration.GetConnectionString(Enum.GetName(typeof(DbOptions), TargetDbName)));
                        PeopleContext p = new PeopleContext(optionBuilder.Options);
                        return new PeopleService(p);
                    }).As<IPeopleService>();
                    break;

                case DbOptions.SqlServer:
                    builder.Register(ctx =>
                    {
                        var optionBuilder = new DbContextOptionsBuilder<PeopleContext>();
                        optionBuilder.UseSqlServer(Configuration.GetConnectionString(Enum.GetName(typeof(DbOptions), TargetDbName)));
                        PeopleContext p = new PeopleContext(optionBuilder.Options);
                        return new PeopleService(p);
                    }).As<IPeopleService>();
                    break;

                case DbOptions.MongoDb:
                    builder.Register(ctx =>
                    {
                        return new MgPeopleService(Configuration);
                    }).As<IPeopleService>();
                    break;
            }

            builder.RegisterType<LogHelper>().As<ILogHelper>();
            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
            #endregion
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
