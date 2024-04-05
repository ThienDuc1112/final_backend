using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Recruitment.ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChatController : Controller
    {

        [HttpGet("{userMessage}")]
        public async Task GetResponse(string userMessage)
        {
            HttpContext.Response.Headers.Add("Content-Type", "text/event-stream");
            OpenAIClient client = new OpenAIClient(
              new Uri("https://job-assistant.openai.azure.com/"),
              new AzureKeyCredential("ef25286ceaaa496895e401f4c8147741"));
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-35-turbo-16k",
                Messages =
                    {
                         new ChatRequestSystemMessage("You are an AI assistant that helps people find information."),
                         new ChatRequestUserMessage(userMessage),
                    },
                MaxTokens = 400,
            };

            await foreach (StreamingChatCompletionsUpdate chatUpdate in await client.GetChatCompletionsStreamingAsync(chatCompletionsOptions))
            {
                if (chatUpdate.ContentUpdate != null)
                {
                    string updateString = chatUpdate.ContentUpdate + Environment.NewLine;
                    byte[] updateBytes = Encoding.UTF8.GetBytes(updateString);
                    await HttpContext.Response.BodyWriter.WriteAsync(updateBytes);
                    await HttpContext.Response.BodyWriter.FlushAsync();
                }
            }

        }
    }
}
