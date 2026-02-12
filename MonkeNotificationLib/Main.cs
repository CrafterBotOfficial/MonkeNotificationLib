using BepInEx;
using BepInEx.Logging;

namespace MonkeNotificationLib;

[BepInPlugin("crafterbot.notificationlib", "MonkeNotificationLib", "1.1.0")]
internal class Main : BaseUnityPlugin
{
    public static Main Instance;

    private NotificationManager manager;

    private void Start()
    {
        Instance = this;
        GorillaTagger.OnPlayerSpawned(async () =>
        {
            manager = NotificationManager.Instance;

            // repeat debug line 10 times
#if DEBUG
            for (int i = 0; i < 10; i++)
            {
                NotificationController.AppendMessage("MonkeNotificationLib", "Test " + i);
                await System.Threading.Tasks.Task.Delay(500);
            }
        });
#endif
    }

    public static void Log(string message, LogLevel level = LogLevel.Info)
    {
        Instance.Logger.Log(level, message);
    }

    private void OnEnable() { }
    private void OnDisable() { }
}
