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

            var result = true;

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(c4p.Url);
            var htmlBody= htmlDoc.DocumentNode.SelectSingleNode("//body");

            OpenAIClient client = new OpenAIClient(new Uri(this.config.Endpoint),
                new AzureKeyCredential(this.config.Key));

            var userMessage = Prompt.PromptMessage.Replace("<HTML Placeholder>", htmlBody.InnerHtml);

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                         new ChatRequestSystemMessage(Prompt.SystemMessage),
                         new ChatRequestUserMessage(userMessage)
                     },
                Temperature = 0.0f,
                MaxTokens = 10000,
                ChoiceCount = 1,
                DeploymentName = config.ModelName
            };

            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);

            var completions = response.Value;
            logger.LogInformation($"OpenAI usage: TotalTokens={0}, PromptTokens={1}, CompletionTokens={2}",completions.Usage.TotalTokens, completions.Usage.PromptTokens, completions.Usage.CompletionTokens);

            var mainChoice = completions.Choices.FirstOrDefault();

            if (mainChoice != null)
            {
                var responseMessage = mainChoice.Message;
                if (!string.IsNullOrWhiteSpace(responseMessage.Content) )
                {
                    var jsonContent = responseMessage.Content;
                    if (!jsonContent.StartsWith("{"))
                    {
                        jsonContent = $"{{{jsonContent}";
                    }
                    var entity = JsonSerializer.Deserialize<C4PEntity>(jsonContent);
                    if (entity!=null)
                    {
                        c4p.EventName = entity.eventName;
                        c4p.EventDate = DateTime.Parse(entity.eventDate);
                        c4p.EventLocation = entity.eventLocation;
                        c4p.ExpiredDate = DateTime.Parse(entity.c4pExpirationDate);
                    }
                    
                }
            }
            return result;
        }
    }
}
