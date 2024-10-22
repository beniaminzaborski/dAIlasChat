using Microsoft.SemanticKernel;
using MyChatAppWithKernel.Plugins;

namespace MyChatAppWithKernel.Extensions;

public static class KernelExtensions
{
    public static void UseKernelPlugins(this Kernel kernel)
    {
        kernel.Plugins
            .AddFromType<MenuPlugin>(serviceProvider: kernel.Services);
    }
}
