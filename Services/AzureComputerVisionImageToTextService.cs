using Azure.AI.Vision.ImageAnalysis;
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using Microsoft.Extensions.Options;
using Bz.dAIlasChat.Configuration;

namespace Bz.dAIlasChat.Services;

public class AzureComputerVisionImageToTextService(IOptions<ImageToTextOptions> options) : IImageToTextService
{
    private readonly string _azureServiceEndpoint = options.Value.ServiceUri;
    private readonly string _azureServiceApiKey = options.Value.ServiceApiKey;

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
