using System;
using System.Net;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using FightSearch.Api.helpers;
using FightSearch.Api.Middlewear;
using FightSearch.Common;
using FightSearch.Common.Settings;
using FightSearch.Repository.Sql;
using FightSearch.Repository.SqlLight;
using FightSearch.Service;
using FightSearch.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Swagger;
using WebEssentials.AspNetCore.Pwa;

namespace FightSearch.Api
{
    

    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? Configuration.GetConnectionString("DefaultConnection");
            Debug.WriteLine(connectionString);
            SettingsProvider.SetLoggerFactory(loggerFactory);
            //SettingsProvider.SetConfiguration(configuration);
            //SettingsProvider.SetHostingEnvironment(hostingEnvironment);

            //using (var client = new UfcContextLite())
            //{
            //    //Create the database file at a path defined in SimpleDataStorage
            //    client.Database.EnsureCreated();
            //    //Create the database tables defined in SimpleDataStorage
            //    client.Database.Migrate();
            //}
        }
       
        private IConfiguration Configuration { get; }
        private readonly string corsPolicy = "Cors";

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            //X-Forwarded-Host – the original host name
            //X-Forwarded-Proto – the original scheme (http or https)
            //X-Forwarded-For – the original IP
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (!environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandler>();
            app.UseCors(corsPolicy);
            app.UseStaticFiles(new StaticFileOptions()
            {
                
                //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
            });
            
            
            //var dir = Directory.GetCurrentDirectory();
            //app.Use(async (context, next) =>
            //{
            //    var queryPath = context.Request.Path.ToString(); 
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //    Debug.WriteLine(queryPath);
                
            //});

            // switch this to use and it wont terminate the request
            // app.Run does
            //app.Run(async context =>
            //{

            //    await context.Response.WriteAsync("Hello from 2nd delegate.");
            //});
            
            

            app.UseSwagger(c => { c.RouteTemplate = "/api/swagger/{documentname}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Fight Search Api V1");
                c.RoutePrefix = "api/swagger/ui";
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}");
                #if !DEBUG
                routes.MapRoute("Spa", "{*url}", defaults: new { controller = "Home", action = "Spa" });
                #endif
            });
            
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? Configuration.GetConnectionString("DefaultConnection");
            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders = 
            //        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //    //options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:100.64.0.0"), 106));
            //});
            //services.AddDbContext<UfcContextLite>(options => options.UseSqlite("data source=.\\UfcSqlite.sqlite"));

            services.AddMvc(options =>
            {
                options.Conventions.Add(new ApiExplorerIgnores());
                //options.Filters.Add(new FluentValidateAttribute());
                options.OutputFormatters.RemoveType<TextOutputFormatter>();
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //options.SerializerSettings.Formatting = Formatting.Indented;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IFileProvider>(  
                new PhysicalFileProvider(Directory.GetCurrentDirectory()));
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, builder => builder
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
            //services.AddDbContext<UfcContext>(options =>
            //{
            //    //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //    options.UseSqlServer(connectionString);
            //    //IHostingEnvironment hostingEnvironment = SettingsProvider.HostingEnvironment;
            //    //ILoggerFactory loggerFactory = SettingsProvider.LoggerFactory;
                
            //    // Enables application data to be included in exception messages, logging, etc. This can include the values assigned to properties of your entity instances,
            //    // parameter values for commands being sent to the database, and other such data. You should only enable this flag
            //    // if you have the appropriate security measures in place based on the sensitivity of this data.
            //    options.EnableSensitiveDataLogging(SettingsProvider.HostingEnvironment.IsDevelopment());
            //    //Enables detailed errors when handling of data value exceptions that occur during processing of store query results. Such errors
            //    //most often occur due to misconfiguration of entity properties. E.g. If a property is configured to be of type
            //    //'int', but the underlying data in the store is actually of type 'string', then an exception will be generated
            //    //at runtime during processing of the data value. When this option is enabled and a data error is encountered, the
            //    //generated exception will include details of the specific entity property that generated the error.
            //    //Enabling this option incurs a small performance overhead during query execution.
            //    options.EnableDetailedErrors(SettingsProvider.HostingEnvironment.IsDevelopment());
            //    //options.UseLoggerFactory(GetConsoleLoggerFactory());
            //    options.UseLoggerFactory(SettingsProvider.LoggerFactory);
            //    options.ConfigureWarnings(warn => { warn.Default(WarningBehavior.Log); });
            //});

            // config sqlite
            DbContextOptionsBuilder<UfcContextLite> optionsBuilder = new DbContextOptionsBuilder<UfcContextLite>()
                .UseSqlite(Configuration.GetConnectionString("SqLiteDefaultConnection"));
            UfcContextLite context = new UfcContextLite(optionsBuilder.Options);
            context.Database.EnsureCreated();
            services.AddSingleton(optionsBuilder.Options);
                    
            //services.AddDbContextPool<UfcContextLite>(options => options.UseSqlite(Configuration.GetConnectionString("SqLiteDefaultConnection")));
            services.AddDbContextPool<UfcContextLite>(options => options.UseSqlite(Configuration.GetConnectionString("SqLiteDefaultConnection")));
            services.AddScoped<UfcContextLite>();
            //services.AddSingleton<UfcContextLite>(context);


            //services.AddDbContext<UfcContext>(options => options.UseSqlServer(connectionString));
            //services.AddScoped<IFightSearchEntities, UfcContext>();
            //services.AddScoped<IVisitorTrackingService, VisitorTrackingService>();
            services.AddScoped<IVisitorTrackingService, VisitorTrackingServiceSqLite>();
            
            //services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISearchService, SearchServiceSqLite>();
            //services.AddScoped<IFighterNameService, FighterNameService>();
            services.AddScoped<IFighterNameService, FighterNameServiceSqLite>();
            //services.AddProgressiveWebApp(new PwaOptions
            //{
            //    RegisterServiceWorker = false,
            //    RegisterWebmanifest = false,
            //    OfflineRoute = "offline"
            //});
        }
    }
}