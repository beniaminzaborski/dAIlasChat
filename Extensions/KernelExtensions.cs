using Microsoft.SemanticKernel;
using Bz.dAIlasChat.Plugins;

namespace Bz.dAIlasChat.Extensions;

public static class KernelExtensions
{
    public static void UseKernelPlugins(this Kernel kernel)
    {
        kernel.Plugins
            .AddFromType<MenuPlugin>(serviceProvider: kernel.Services);
        kernel.Plugins
            .AddFromType<SpeechOrderPlugin>(serviceProvider: kernel.Services);
    }
}
