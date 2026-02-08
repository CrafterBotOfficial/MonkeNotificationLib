using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace MonkeNotificationLib;

[BepInPlugin("crafterbot.notificationlib", "MonkeNotificationLib", "1.0.7")]
internal class Main : BaseUnityPlugin
{
    public static Main Instance;

    private NotificationManager manager;

    private void Start()
    {
        Instance = this;
        GorillaTagger.OnPlayerSpawned(() =>
        {
            manager = new NotificationManager();
            //NotificationController.AppendMessage("MonkeNotificationLib", "Loaded!");
        });
    }

    public static void Log(string message, LogLevel level = LogLevel.Info)
    {
        Instance.Logger.Log(level, message);
    }

    private void OnEnable() { }
    private void OnDisable() { }
}
