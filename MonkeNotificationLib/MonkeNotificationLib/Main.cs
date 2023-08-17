using BepInEx;
using BepInEx.Logging;

namespace MonkeNotificationLib
{
    [BepInPlugin("crafterbot.notificationlib", "MonkeNotificationLib", "1.0.2")]
    internal class Main : BaseUnityPlugin
    {
        internal static Main Instance;

        private void Start()
        {
            Instance = this;
            new HarmonyLib.Harmony(Info.Metadata.GUID).PatchAll(typeof(Patches));
        }

        internal static void Log(object data, LogLevel logLevel = LogLevel.Info)
        {
            if (Instance is object)
            {
                Instance.Logger.Log(logLevel, data);
                return;
            }
            UnityEngine.Debug.Log($"[NotificationLib : {System.Enum.GetName(typeof(LogLevel), loglevel)}]" + data);
        }
        
        #region Enable/Disable
        private void OnEnable() { }
        private void OnDisable() { }
        #endregion
    }
}
