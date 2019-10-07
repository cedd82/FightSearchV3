using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;

namespace FightSearch.Api.Middlewear
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment hostingEnvironment)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, $"Exception occurred message: {exception.Message}");
                
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Data = new {},
                Status = new
                {
                    Success = false,
                    Error = "Something went wrong",
                    ErrorCode = 1,
                    Timestamp = DateTime.Now.ToString("s")
                }
            }));
        }
    }
}
