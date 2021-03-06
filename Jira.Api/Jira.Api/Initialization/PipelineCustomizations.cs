﻿using System;
using System.Text;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace Jira.Api.Initialization
{
    public class PipelineCustomizations : IApplicationStartup
    {
        private readonly object _lockObject = new object();

        public void Initialize(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                lock (_lockObject)
                {
                    WriteRequest(ctx);

                    Console.WriteLine();
                }

                return null;
            });

            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                WriteError(ex);

                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline(AddAccessControlHeaders);
        }

        private static void AddAccessControlHeaders(NancyContext ctx)
        {
            ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        }

        private void WriteRequest(NancyContext ctx)
        {
            Console.WriteLine($"{ctx.Request.Method} request to {ctx.Request.Url}");
        }

        private static void WriteError(Exception ex)
        {
            Console.WriteLine("An exception occured.");
            WriteException(ex);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                WriteException(ex);
            }
        }

        private static void WriteException(Exception ex)
        {
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"Source: {ex.Source}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
        }
    }
}