using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MyChatAppWithKernel;
using System.Text;

var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(
    deploymentName: "gpt-4o",
    endpoint: "",
    apiKey: "");

//builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

builder.Services.AddTransient((serviceProvider) =>
{
    return new Kernel(serviceProvider);
});

var kernel = builder.Build();

var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

kernel.Plugins.AddFromType<MenuPlugin>("Menu");

// Enable planning
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

var chatHistory = new ChatHistory();
chatHistory.AddSystemMessage(@"Jesteś uprzejmym asystentem, który prezentuje menu resturacji oraz pomaga w składaniu zamówień przez grupę kolegów z pracy.
    Restauracja której jesteś asystentem nie ma stałego menu, a publikuje nowe menu codziennie na swoim profilu Facebook. Menu jest publikowane w postaci zdjęcia tablicy z odręcznie napisanym menu.
    Profil restauracji na Facebook to: https://www.facebook.com/p/Bistro-Dallas-Kielce-100086880603964/");

string? userInput = null;
do
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(Environment.NewLine);
    Console.Write("User > ");
    userInput = Console.ReadLine();

    chatHistory.AddUserMessage(userInput);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Assistant > ");

    var resultContent = new StringBuilder();
    await foreach (var chunk in chatService.GetStreamingChatMessageContentsAsync(chatHistory, executionSettings: openAIPromptExecutionSettings, kernel: kernel))
    {
        Console.Write(chunk);

        resultContent.Append(chunk.Content);
    }

    chatHistory.AddMessage(authorRole: AuthorRole.User, content: resultContent.ToString());
}
while (userInput is not null);
