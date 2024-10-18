using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using MyChatAppWithKernel.Services;

namespace MyChatAppWithKernel.Extensions;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder AddAzureComputerVisionImageToText(
        this IKernelBuilder kernelBuilder)
    {
        kernelBuilder.Services.AddTransient<IImageToTextService, AzureComputerVisionImageToTextService>();
        return kernelBuilder;
    }
}
