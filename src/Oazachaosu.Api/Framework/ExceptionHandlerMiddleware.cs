﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Core.Common;

namespace Oazachaosu.Api.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                logger.Debug($"Request: {context.Request.Path}");
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = ErrorCode.Undefined;
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch (exception)
            {
                case Exception e when exceptionType == typeof(Exception):
                    logger.Error(exception, $"Message: {exception.Message} | StackTrace: {exception.StackTrace}");
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ApiException e when exceptionType == typeof(ApiException):
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = e.Code;
                    break;
            }
            var response = new { code = errorCode, message = CollectExceptionsInformation(exception) };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }

        private string CollectExceptionsInformation(Exception exception)
        {
            if(exception.InnerException == null)
            {
                return $"'{exception.Message}'\n";
            }
            string result = $"'{exception.Message}'\n{CollectExceptionsInformation(exception.InnerException)}";
            return result;
        } 
    }
}