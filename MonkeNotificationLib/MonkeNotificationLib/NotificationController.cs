using System.Collections.Generic;

namespace MonkeNotificationLib
{
    public static class NotificationController
    {
        public static void AppendMessage(string source, string message, bool includeTimeStamp = false)
        {
            string timeStampt = includeTimeStamp ? $"[{System.DateTime.Now.ToString("hh:mm:ss")} : " : "";
            string messageFormat = $"<b>[{timeStampt}{source}]</b> {message}";
            NotificationManager.Instance?.NewLine(messageFormat);
        }
        public static void AppendMessage(string message) =>
            NotificationManager.Instance?.NewLine(message);


        /* Extension methods */

        public static string WrapColor(this string str, string color)
        {
            Dictionary<string, string> colorMap = new Dictionary<string, string>()
            {
                { "green", "09ff00" },
                { "red", "ff0800" },
                { "gray", "ffffff50" }
            };
            colorMap.TryGetValue(color, out color);
            return $"<color={color}>{str}</color>";
        }
    }
}
