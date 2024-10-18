using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using MyChatAppWithKernel.Configuration;
using MyChatAppWithKernel.Services;

namespace MyChatAppWithKernel.Extensions;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder AddAzureComputerVisionImageToText(
        this IKernelBuilder kernelBuilder, IConfiguration configuration)
    {
        kernelBuilder.Services.Configure<ImageToTextOptions>(
            configuration.GetSection(ImageToTextOptions.ImageToText));

        kernelBuilder.Services.AddTransient<IImageToTextService, AzureComputerVisionImageToTextService>();
        return kernelBuilder;
    }
}
