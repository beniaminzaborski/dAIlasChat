using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using Bz.dAIlasChat.Configuration;
using Bz.dAIlasChat.Services;
using Microsoft.SemanticKernel.TextToAudio;

namespace Bz.dAIlasChat.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddOptions(config)
            .AddAiServices(config)
            /*.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace))*/;
    }

    private static IServiceCollection AddAiServices(this IServiceCollection services, IConfiguration config)
    {
        var azureOpenAIChatOptions = new AzureOpenAIChatOptions();
        config.GetSection(AzureOpenAIChatOptions.SectionName).Bind(azureOpenAIChatOptions);

        return services
            .AddAzureOpenAIChatCompletion(
                deploymentName: azureOpenAIChatOptions.DeploymentName,
                endpoint: azureOpenAIChatOptions.ServiceUri,
                apiKey: azureOpenAIChatOptions.ServiceApiKey)
            .AddTransient<IImageToTextService, AzureComputerVisionImageToTextService>()
            .AddTransient<ITextToAudioService, AzureTextToSpeechService>();
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<AzureOpenAIChatOptions>(
                config.GetSection(AzureOpenAIChatOptions.SectionName))
            .Configure<ImageToTextOptions>(
                config.GetSection(ImageToTextOptions.SectionName))
            .Configure<TextToSpeechOptions>(
                config.GetSection(TextToSpeechOptions.SectionName));
    }
}
