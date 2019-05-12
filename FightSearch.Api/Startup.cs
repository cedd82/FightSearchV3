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
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
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
                
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fight Search Api", Version = "v1" });
            });
            services.Configure<ImagePaths>(Configuration.GetSection("ImagePaths"));
            services.AddEntityFrameworkSqlServer();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UfcContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IFightSearchEntities, UfcContext>();
            services.AddScoped<IVisitorTrackingService, VisitorTrackingService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IFighterNameService, FighterNameService>();
        }
    }
}