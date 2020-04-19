using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWare3.Services;
using MiddleWare3.Models;

namespace MiddleWare3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IOService services)
        {
            httpContext.Request.EnableBuffering();
            if (httpContext.Request != null)
            {
                Logging logging = new Logging();
                logging.Path = httpContext.Request.Path;// api/studenst
                logging.Method = httpContext.Request.Method;//get, post etc.
                logging.QueryString = httpContext.Request.QueryString.ToString();
                logging.Date = DateTime.Now;

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                
                    logging.Body = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }
                string log = JsonConvert.SerializeObject(logging);
                services.Write(log);
        }
           await _next(httpContext);

    }
}
}
