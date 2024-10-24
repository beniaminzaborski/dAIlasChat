namespace Bz.dAIlasChat.Configuration;

public class ImageToTextOptions
{
    public const string SectionName = "ImageToText";

    public string ServiceUri { get; set; } = String.Empty;
    public string ServiceApiKey { get; set; } = String.Empty;
}
