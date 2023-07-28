using System.Collections.Generic;
using UnityEngine.UI;

namespace MonkeNotificationLib
{
    /// <summary> API for MonkeNotificationLib </summary>
    public static class NotificationController
    {
        /// <summary>
        /// Pulls a text object from the object pool and sets the text to '[{timestamp : {source}] {message}'
        /// </summary>
        /// <param name="includeTimeStamp">Default false | Should there be a timestamp?</param>
        /// <returns>The text that was pulled from the pool.</returns>
        public static Text AppendMessage(string source, string message, bool includeTimeStamp = false)
        {
            string timeStampt = includeTimeStamp ? $"[{System.DateTime.Now.ToString("hh:mm:ss")} : " : "";
            string messageFormat = $"<b>[{timeStampt}{source}]</b> {message}";
            return NotificationManager.Instance?.NewLine(messageFormat);
        }
        /// <summary>
        /// Pulls a text object from the object pool and sets the text to your text.
        /// </summary>
        /// <returns>The text that was pulled from the pool.</returns>
        public static Text AppendMessage(string message) =>
            NotificationManager.Instance?.NewLine(message);


        /* Extension methods */

        /// <summary>Wraps your string in <color="color"></color>. Read the methods code from the Github page to see the presets. 
        /// (may add more later)</summary>
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
