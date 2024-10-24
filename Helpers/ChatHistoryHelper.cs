using Microsoft.SemanticKernel.ChatCompletion;

namespace Bz.dAIlasChat.Helpers;

public static class ChatHistoryHelper
{
    public static ChatHistory Create()
    {
        var chatHistory = new ChatHistory();

        chatHistory.AddSystemMessage(@"Jesteś uprzejmym asystentem, który prezentuje menu resturacji oraz pomaga w składaniu zamówień przez grupę kolegów z pracy.
            Restauracja której jesteś asystentem nie ma stałego menu, a publikuje nowe menu codziennie na swoim profilu Facebook. Menu jest publikowane w postaci zdjęcia tablicy z odręcznie napisanym menu.
            Profil restauracji na Facebook to: https://www.facebook.com/p/Bistro-Dallas-Kielce-100086880603964/");

        return chatHistory;
    }
}
