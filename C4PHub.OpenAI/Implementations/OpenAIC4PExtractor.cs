using Azure.AI.OpenAI;
using Azure;
using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.OpenAI.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;
using System.Text.Json;
using C4PHub.OpenAI.Entities;
using HtmlAgilityPack;
using Microsoft.SemanticKernel;
using System.Net;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace C4PHub.OpenAI.Implementations
{
    public class OpenAIC4PExtractor : IC4PExtractor
    {
        private readonly OpenAIServiceConfiguration config;
        private readonly ILogger<OpenAIC4PExtractor> logger;

        public OpenAIC4PExtractor(IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<OpenAIC4PExtractor>();
            this.config = OpenAIServiceConfiguration.Load(configuration);
        }

        public Task<bool> CanManagedC4PAsync(C4PInfo c4p, CancellationToken token = default)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> FillC4PAsync(C4PInfo c4p, CancellationToken token = default)
        {
            if (c4p.Url == null)
                throw new ArgumentException("Url is null");
            this.logger.LogInformation("Extracting C4P from {0}", c4p.Url);

            var result = true;
            HtmlNode htmlBody = await ExtractBodyFromUrl(c4p);

            var kernelBuilder = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(this.config.ModelName, this.config.Endpoint, this.config.Key)
                .Build();

            var extractorFunction = kernelBuilder.CreateFunctionFromPrompt(Prompt.PromptMessage,
               new OpenAIPromptExecutionSettings()
               {
                   ChatSystemPrompt = Prompt.SystemMessage,
                   Temperature = 0.0f,
                   MaxTokens = 10000
               });

            KernelArguments arguments = new KernelArguments()
            {
                {"input", this.config.UseHtml ? htmlBody.InnerHtml : htmlBody.InnerText}
            };

            var response = await kernelBuilder.InvokeAsync(extractorFunction, arguments);

            var responseMessage = response.GetValue<string>();
            if (!string.IsNullOrWhiteSpace(responseMessage))
            {
                DeserializeResponse(c4p, responseMessage);
            }
            this.logger.LogInformation($"Extracted C4P from {0}: {1} - {2} - {3} - {4}", c4p.Url, c4p.EventName, c4p.EventDate, c4p.EventLocation, c4p.ExpiredDate);
            return result;
        }

        private void DeserializeResponse(C4PInfo c4p, string responseMessage)
        {
            if (!responseMessage.StartsWith("{"))
            {
                responseMessage = $"{{{responseMessage}";
            }
            var entity = JsonSerializer.Deserialize<C4PEntity>(responseMessage);
            if (entity != null)
            {
                c4p.EventName = entity.eventName;
                if (!string.IsNullOrWhiteSpace(entity.eventDate))
                {
                    if (DateTime.TryParse(entity.eventDate, out var eventDate))
                        c4p.EventDate = eventDate;
                }
                c4p.EventLocation = entity.eventLocation;
                if (!string.IsNullOrWhiteSpace(entity.c4pExpirationDate))
                {
                    if (DateTime.TryParse(entity.c4pExpirationDate, out var expiredDate))
                        c4p.ExpiredDate = expiredDate;
                }
            }
        }

        private async Task<HtmlNode> ExtractBodyFromUrl(C4PInfo c4p)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(c4p.Url);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

            this.logger.LogInformation("Extracting from body {0}", htmlBody.InnerHtml);
            return htmlBody;
        }
    }
}
