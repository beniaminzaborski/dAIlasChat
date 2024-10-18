using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ImageToText;
using System.ComponentModel;

namespace MyChatAppWithKernel.Plugins;

public class MenuPlugin(IImageToTextService imageToTextService)
{
    private readonly IImageToTextService _imageToTextService = imageToTextService;

    [KernelFunction("get_menu")]
    [Description("Pobiera aktualne menu na dziś")]
    [return: Description("Aktualne menu na dziś")]
    public async Task<IEnumerable<string?>> GetMenuItems()
    {
        const string lastMenuImageUri = "https://scontent-waw2-1.xx.fbcdn.net/v/t39.30808-6/463750881_511767991729217_8202002394917812110_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=833d8c&_nc_ohc=R6cPwn4hmFYQ7kNvgE_385A&_nc_zt=23&_nc_ht=scontent-waw2-1.xx&_nc_gid=AdeXO19Qgulvdjz6HHO45AQ&oh=00_AYBub_vfjpBZ_C-O0rccIRRDKKeU0Dykalyvyta4LHEXxA&oe=67187E8A";

        var ocrResult = await _imageToTextService.GetTextContentsAsync(
            new ImageContent(new Uri(lastMenuImageUri)));

        return ocrResult.Select(t => t.Text);
    }
}
