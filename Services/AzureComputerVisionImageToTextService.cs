using Azure.AI.Vision.ImageAnalysis;
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using Microsoft.Extensions.Configuration;

namespace MyChatAppWithKernel.Services;

public class AzureComputerVisionImageToTextService : IImageToTextService
{
    private readonly string _azureServiceEndpoint;
    private readonly string _azureServiceApiKey;

    public AzureComputerVisionImageToTextService(/*IConfiguration configuration*/)
    {
        //_azureServiceEndpoint = configuration["OcrServicesEndpoint"];
        //_azureServiceApiKey = configuration["OcrServicesKey"];
        _azureServiceEndpoint = "";
        _azureServiceApiKey = "";
    }

    public IReadOnlyDictionary<string, object?> Attributes => throw new NotImplementedException();

    public async Task<IReadOnlyList<TextContent>> GetTextContentsAsync(
        ImageContent content,
        PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default)
    {
        ImageAnalysisClient client = new ImageAnalysisClient(
            new Uri(_azureServiceEndpoint),
            new AzureKeyCredential(_azureServiceApiKey));

        ImageAnalysisResult result = await client.AnalyzeAsync(content.Uri, VisualFeatures.Read);

        return result.Read.Blocks.SelectMany(b => b.Lines).Select(l => new TextContent(l.Text)).ToList();
    }
}
