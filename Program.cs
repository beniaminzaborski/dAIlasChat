using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;

var builder = Kernel.CreateBuilder();

builder.Services.AddAzureOpenAIChatCompletion(
    deploymentName: "gpt-4o",
    endpoint: "",
    apiKey: "",
    modelId: "gpt-4o");

builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

builder.Services.AddTransient((serviceProvider) =>
{
    return new Kernel(serviceProvider);
});

var kernel = builder.Build();

var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

var chatHistory = new ChatHistory();

string? userInput = null;
do
{
    Console.Write(Environment.NewLine);
    Console.Write("User > ");
    userInput = Console.ReadLine();

    chatHistory.AddUserMessage(userInput);

    Console.Write("Assistant > ");

    var resultContent = new StringBuilder();
    await foreach (var chunk in chatService.GetStreamingChatMessageContentsAsync(chatHistory, kernel: kernel))
    {
        Console.Write(chunk);

        resultContent.Append(chunk.Content);
    }

    chatHistory.AddMessage(authorRole: AuthorRole.User, content: resultContent.ToString());
}
while (userInput is not null);
