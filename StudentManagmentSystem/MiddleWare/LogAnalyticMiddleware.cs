using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using StudentManagmentSystem.Models;
using StudentManagmentSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.MiddleWare
{
    public class LogAnalyticMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private Func<object, Exception, object> _defaultFormatter = (state, exception) => state;

        public LogAnalyticMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<LogAnalyticMiddleware>();
        }

        public async Task Invoke(HttpContext context, LogAnalyticService logAnalyticService)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            _logger.Log(LogLevel.Information, 1, $"REQUEST METHOD: {context.Request.Method}, REQUEST BODY: {requestBodyText}, REQUEST URL: {url}", null, _defaultFormatter);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);
            context.Request.Body = originalRequestBody;
            var logAnalytics = new LogAnalytic
            {
                RowGuid = Guid.NewGuid(),
                Url = url,
                //UserId = session.LoginUserId,
                //UserName = session.LoginUserName,
                //IPAddress = session.LoginUserIpAddress,
                //Device = session.LoginUserDeviceType,
                //GeographicLocation = "",
                //Browser = session.LoginUserBrowser
            };
            await logAnalyticService.AddAsync(logAnalytics);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            //var body = request.Body;
            //request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            //request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }
    }
}
