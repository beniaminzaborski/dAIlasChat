namespace Bz.dAIlasChat.Configuration;

public class TextToSpeechOptions
{
    public const string SectionName = "TextToSpeech";

    public string ServiceUri { get; set; } = String.Empty;
    public string ServiceApiKey { get; set; } = String.Empty;
    public string SpeechRegion { get; set; } = String.Empty;
}