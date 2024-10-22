using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using MyChatAppWithKernel.Configuration;
using MyChatAppWithKernel.Plugins;
using MyChatAppWithKernel.Services;

namespace MyChatAppWithKernel.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddOptions(config)
            .AddAiServices(config)
            .AddSemanticKernelPlugins()
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
            .AddTransient<IImageToTextService, AzureComputerVisionImageToTextService>();
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<AzureOpenAIChatOptions>(
                config.GetSection(AzureOpenAIChatOptions.SectionName))
            .Configure<ImageToTextOptions>(
                config.GetSection(ImageToTextOptions.SectionName));
    }

    private static IServiceCollection AddSemanticKernelPlugins(this IServiceCollection services)
    {
        return services
            /*.AddTransient<MenuPlugin>()*/;
    }
}
