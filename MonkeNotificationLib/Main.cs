using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MonkeNotificationLib;

[BepInPlugin("crafterbot.notificationlib", "MonkeNotificationLib", "1.0.2")]
internal class Main : BaseUnityPlugin
{
    public static Main Instance;
    private Harmony harmony;

    private void Start()
    {
        Instance = this;

        harmony = new Harmony(Info.Metadata.GUID);
        harmony.PatchAll();
    }

    public static void Log(object data, LogLevel level = LogLevel.Info)
    {
        Instance?.Logger.Log(level, data);
    }

    #region Enable/Disable
    private void OnEnable() { }
    private void OnDisable() { }
    #endregion
}
