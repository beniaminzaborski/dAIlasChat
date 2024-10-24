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
        const string lastMenuImageUri = "https://scontent-waw2-2.xx.fbcdn.net/v/t39.30808-6/464250805_514720228100660_8919845461864817507_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=833d8c&_nc_ohc=QXTciiZKhwYQ7kNvgEKraya&_nc_zt=23&_nc_ht=scontent-waw2-2.xx&_nc_gid=AQKyeiJ82R8rxDIXxmI4OpB&oh=00_AYAJxjwuZQWiuXju4oy46e3dfvFiNHIl9qj0NTAMNSnwLg&oe=671DCECB";

        var ocrResult = await _imageToTextService.GetTextContentsAsync(
            new ImageContent(new Uri(lastMenuImageUri)));

        return ocrResult.Select(t => t.Text);
    }
}
