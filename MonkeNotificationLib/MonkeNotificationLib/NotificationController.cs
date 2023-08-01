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
        /// <param name="fadeOutDelay">How long should the text stay on screen before it begins to fade out?</param>
        /// <returns>The text that was pulled from the pool.</returns>
        public static Text AppendMessage(string source, string message, bool includeTimeStamp = false, float fadeOutDelay = 3)
        {
            string timeStampt = includeTimeStamp ? $"[{System.DateTime.Now.ToString("hh:mm:ss")} : " : "";
            string messageFormat = $"<b>[{timeStampt}{source}]</b> {message}";
            return NotificationManager.Instance?.NewLine(messageFormat, fadeOutDelay);
        }
        /// <summary>
        /// Pulls a text object from the object pool and sets the text to your text.
        /// </summary>
        /// <param name="fadeOutDelay">How long should the text stay on screen before it begins to fade out?</param>
        /// <returns>The text that was pulled from the pool.</returns>
        public static Text AppendMessage(string message, float fadeOutDelay) =>
            NotificationManager.Instance?.NewLine(message, fadeOutDelay);


        /* Extension methods */

        /// <summary>Wraps your string in <color="color"></color>. Read the methods code from the Github page to see the presets. 
        /// (may add more later)</summary>
        public static string WrapColor(this string str, string color)
        {
            // Also I know Unity has rich text colors, but I perfer these colors over the presets.
            Dictionary<string, string> colorMap = new Dictionary<string, string>()
            {
                { "green", "09ff00" },
                { "red", "ff0800" },
                { "gray", "ffffff50" }
            };
            colorMap.TryGetValue(color.ToLower(), out color);
            return $"<color=#{color}>{str}</color>";
        }
    }
}
