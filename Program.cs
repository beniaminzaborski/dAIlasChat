using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using MyChatAppWithKernel.Extensions;
using MyChatAppWithKernel.Helpers;
using System.Text;

var builder = Kernel.CreateBuilder();
var services = builder.Services;
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

services.AddServices(config);

var kernel = builder.Build();

kernel.UseKernelPlugins();

var chatHistory = ChatHistoryHelper.Create();
var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = OpenAIPromptExecutionSettingsHelper.Create();

string userInput = string.Empty;
do
{
    Console.Write(Environment.NewLine);

    ConsoleHelper.PrintAsUser();
    userInput = Console.ReadLine();

    chatHistory.AddUserMessage(userInput);

    ConsoleHelper.PrintAsAssistant();

    var resultContent = new StringBuilder();
    await foreach (var chunk in chatService.GetStreamingChatMessageContentsAsync(chatHistory, executionSettings: openAIPromptExecutionSettings, kernel: kernel))
    {
        Console.Write(chunk);

        resultContent.Append(chunk.Content);
    }

    chatHistory.AddMessage(authorRole: AuthorRole.User, content: resultContent.ToString());
}
while (userInput.ToLower() != "bye");
