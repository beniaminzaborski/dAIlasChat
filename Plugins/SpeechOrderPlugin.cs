using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToAudio;

namespace Bz.dAIlasChat.Plugins;

public class SpeechOrderPlugin(ITextToAudioService textToAudioService)
{
    private readonly ITextToAudioService _textToAudioService = textToAudioService;
    
    [KernelFunction("say_order_summary")]
    [Description("Głosowo odczytuje podsumowanie zamówienia")]
    [return: Description("Głosowy odczyt podsumowania zamówienia")]
    public async Task SayOrderSummary(string orderSummaryText)
    {
        await _textToAudioService.GetAudioContentsAsync(orderSummaryText);
    }
}