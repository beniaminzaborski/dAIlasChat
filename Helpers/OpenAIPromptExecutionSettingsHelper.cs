using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Bz.dAIlasChat.Helpers;

public static class OpenAIPromptExecutionSettingsHelper
{
    public static OpenAIPromptExecutionSettings Create()
    {
        return new OpenAIPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };
    }
}
