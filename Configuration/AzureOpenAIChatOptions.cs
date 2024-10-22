namespace MyChatAppWithKernel.Configuration;

public class AzureOpenAIChatOptions
{
    public const string SectionName = "AzureOpenAIChat";

    public string ServiceUri { get; set; } = String.Empty;
    public string ServiceApiKey { get; set; } = String.Empty;
    public string DeploymentName { get; set; } = String.Empty;
}
