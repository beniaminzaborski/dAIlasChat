using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MyChatAppWithKernel;

public class MenuPlugin
{
    [KernelFunction("get_menu")]
    [Description("Pobiera aktualne menu na dziś")]
    [return: Description("Aktualne menu na dziś")]
    public async Task<IEnumerable<string>> GetMenuItems()
    {
        // TODO: Get this from Facebook profile
        return
        [
            "Barszcz ukraiński",
            "Klopsy w sosie paprykowo-pomidorowym (2 szt.) + ryż lub ziemniaki",
            "Stek z karczku z duszoną cebulą i marchewką",
            "Nuggetsy"
        ];
    }
}
