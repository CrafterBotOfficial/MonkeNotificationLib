namespace MonkeNotificationLib;

public static class PluginInfo
{
    public const string GUID = "crafterbot.notificationlib";
    public const string Name = "MonkeNotificationLib";
    public const string Version = "1.0.4";

    public const string Author = "Crafterbot";

    public static void Log(object message, bool isError = false)
    {
#if MELONLOADER
        if (isError) Main.Instance?.LoggerInstance.Error(message);
        else Main.Instance?.LoggerInstance.Msg(message);
#else
        Main.Instance?.Log(message, isError ? BepInEx.Logging.LogLevel.Error : BepInEx.Logging.LogLevel.Info);
#endif
    }
}