namespace FightSearch.Api
{
    using FightSearch.Api.helpers;
    using FightSearch.Common;
    using FightSearch.Common.Settings;
    using FightSearch.Repository.Sql;
    using FightSearch.Service;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            SettingsProvider.SetLoggerFactory(loggerFactory);
            //SettingsProvider.SetConfiguration(configuration);
            SettingsProvider.SetHostingEnvironment(hostingEnvironment);
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("Cors");
            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fight Search Api V1");
                c.RoutePrefix = "swagger/ui";
                //c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(c =>
                    {
                        c.Conventions.Add(new ApiExplorerIgnores());
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder => builder
                                                     .AllowAnyOrigin()
                                                     .AllowAnyMethod()
                                                     .AllowAnyHeader());
            });
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fight Search Api", Version = "v1" });
            });
            services.Configure<ImagePaths>(Configuration.GetSection("ImagePaths"));
            services.AddEntityFrameworkSqlServer();
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UfcContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
                //IHostingEnvironment hostingEnvironment = SettingsProvider.HostingEnvironment;
                //ILoggerFactory loggerFactory = SettingsProvider.LoggerFactory;
                
                // Enables application data to be included in exception messages, logging, etc. This can include the values assigned to properties of your entity instances,
                // parameter values for commands being sent to the database, and other such data. You should only enable this flag
                // if you have the appropriate security measures in place based on the sensitivity of this data.
                options.EnableSensitiveDataLogging(SettingsProvider.HostingEnvironment.IsDevelopment());
                //Enables detailed errors when handling of data value exceptions that occur during processing of store query results. Such errors
                //most often occur due to misconfiguration of entity properties. E.g. If a property is configured to be of type
                //'int', but the underlying data in the store is actually of type 'string', then an exception will be generated
                //at runtime during processing of the data value. When this option is enabled and a data error is encountered, the
                //generated exception will include details of the specific entity property that generated the error.
                //Enabling this option incurs a small performance overhead during query execution.
                options.EnableDetailedErrors(SettingsProvider.HostingEnvironment.IsDevelopment());
                //options.UseLoggerFactory(GetConsoleLoggerFactory());
                options.UseLoggerFactory(SettingsProvider.LoggerFactory);
                options.ConfigureWarnings(warn => { warn.Default(WarningBehavior.Log); });
            });
            //services.AddDbContext<UfcContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IFightSearchEntities, UfcContext>();
            services.AddScoped<IVisitorTrackingService, VisitorTrackingService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IFighterNameService, FighterNameService>();
        }
    }
}