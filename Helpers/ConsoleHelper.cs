namespace MyChatAppWithKernel.Helpers;

public static class ConsoleHelper
{
    public static void PrintAsUser()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("User > ");
    }

    public static void PrintAsAssistant()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Assistant > ");
    }
}
