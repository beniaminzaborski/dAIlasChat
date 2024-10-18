namespace MyChatAppWithKernel.Configuration;

public class ImageToTextOptions
{
    public const string ImageToText = "ImageToText";

    public string ServiceUri { get; set; } = String.Empty;
    public string ServiceApiKey { get; set; } = String.Empty;
}
