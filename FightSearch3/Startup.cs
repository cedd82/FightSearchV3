﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FightSearch.Api
{
    using Microsoft.AspNetCore.HttpOverrides;

    using NLog;

    public class Startup
	{
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
		    Logger logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Error,"xy");
		    app.UseDefaultFiles();
		    app.UseStaticFiles();
		    app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
		    bool test = env.IsDevelopment();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});
		}

	    // This method gets called by the runtime. Use this method to add services to the container.
	    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
	    public void ConfigureServices(IServiceCollection services)
	    {

	    }
	}
}