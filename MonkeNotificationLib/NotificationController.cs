using System.Collections.Generic;

namespace MonkeNotificationLib;

/// <summary> Controls for MonkeNotificationLib </summary>
public static class NotificationController
{
    public const float FADE_DELAY = 1.5f;
    public const string WHITE = "FFFFFF";

    /// <summary>
    /// Pulls a text object from the object pool and sets the text to '[{timestamp : {source}] {message}'
    /// </summary>
    /// <param name="includeTimeStamp">Default false | Should there be a timestamp?</param>
    /// <param name="fadeOutDelay">How long should the text stay on screen before it begins to fade out?</param>
    public static void AppendMessage(string source, string message, bool includeTimeStamp = false, float fadeOutDelay = FADE_DELAY)
    {
        string timeStampt = includeTimeStamp ? $"[{System.DateTime.Now.ToString("hh:mm:ss")} : " : "";
        string messageFormat = $"<b>[{timeStampt}{source}]</b> {message}";
        NotificationManager.Instance?.NewLine(messageFormat, fadeOutDelay); // todo: readd fade out delay
    }

    public static void AppendMessage(string source, string message, string color = WHITE, float fadeOutDelay = FADE_DELAY)
    {
        string messageFormat = $"<b>[{source}]</b> {message}";
        NotificationManager.Instance?.NewLine(messageFormat, fadeOutDelay, color);
    }

    /// <summary>
    /// Pulls a text object from the object pool and sets the text to your text.
    /// </summary>
    /// <param name="fadeOutDelay">How long should the text stay on screen before it begins to fade out?</param>
    public static void AppendMessage(string message, float fadeOutDelay)
    {
        NotificationManager.Instance?.NewLine(message, fadeOutDelay);
    }

    public static void AppendMessage(string message, string color = WHITE, float fadeOutDelay = FADE_DELAY)
    {
        NotificationManager.Instance?.NewLine(message, fadeOutDelay, color);
    }

    /* Color Methods */

    public static string GetColor(string color) =>
        COLOR_MAP_DICTIONARY.TryGetValue(color.ToLower(), out string value) ? value : color;

    private readonly static Dictionary<string, string> COLOR_MAP_DICTIONARY = new Dictionary<string, string>()
    {
        { "green",  "09ff00" },
        { "red",    "ff0800" },
        { "gray",   "808080" },
        { "warning","f0ad4e" },
        { "danger", "d9534f" },
        { "white",  WHITE    },
    };
}
