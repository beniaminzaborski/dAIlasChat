using Bz.dAIlasChat.Configuration;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToAudio;

namespace Bz.dAIlasChat.Services;

public class AzureTextToSpeechService(IOptions<TextToSpeechOptions> options) : ITextToAudioService
{
    private readonly IOptions<TextToSpeechOptions> _options = options;

    public IReadOnlyDictionary<string, object?> Attributes { get; }

    public async Task<IReadOnlyList<AudioContent>> GetAudioContentsAsync(
        string text,
        PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default)
    {
        //var speechConfig = SpeechConfig.Fro(new Uri(_options.Value.ServiceUri), _options.Value.ServiceApiKey);
        var speechConfig = SpeechConfig.FromSubscription(_options.Value.ServiceApiKey, _options.Value.SpeechRegion);
        speechConfig.SpeechSynthesisVoiceName = "pl-PL-ZofiaNeural";
        
        using var speechSynthesizer = new SpeechSynthesizer(speechConfig);
        var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
        //OutputSpeechSynthesisResult(speechSynthesisResult, text);
        
        return new List<AudioContent>{ new AudioContent(speechSynthesisResult.AudioData.AsMemory(), null) };
    }
    
    // static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
    // {
    //     switch (speechSynthesisResult.Reason)
    //     {
    //         case ResultReason.SynthesizingAudioCompleted:
    //             Console.WriteLine($"Speech synthesized for text: [{text}]");
    //             break;
    //         case ResultReason.Canceled:
    //             var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
    //             Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
    //
    //             if (cancellation.Reason == CancellationReason.Error)
    //             {
    //                 Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
    //                 Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
    //                 Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
    //             }
    //             break;
    //         default:
    //             break;
    //     }
    // }
}