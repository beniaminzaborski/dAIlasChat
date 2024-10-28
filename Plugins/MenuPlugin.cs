using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using System.ComponentModel;

namespace Bz.dAIlasChat.Plugins;

public class MenuPlugin(IImageToTextService imageToTextService)
{
    private readonly IImageToTextService _imageToTextService = imageToTextService;

    [KernelFunction("get_menu")]
    [Description("Pobiera aktualne menu na dziś")]
    [return: Description("Aktualne menu na dziś")]
    public async Task<IEnumerable<string?>> GetMenuItems()
    {
        const string lastMenuImageUri = "https://scontent-waw2-2.xx.fbcdn.net/v/t39.30808-6/464777716_519277720978244_8683371289298008173_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=833d8c&_nc_ohc=OdpbDdglbScQ7kNvgFs8bhI&_nc_zt=23&_nc_ht=scontent-waw2-2.xx&_nc_gid=AG6fkHHvhCNSJJv-SOgP20L&oh=00_AYD4poQgcOa1R8RsOhuYRVOCroi_YzsXme2z_UfM2JGg5w&oe=6725CC97";

        var ocrResult = await _imageToTextService.GetTextContentsAsync(
            new ImageContent(new Uri(lastMenuImageUri)));

        return ocrResult.Select(t => t.Text);
    }
}
