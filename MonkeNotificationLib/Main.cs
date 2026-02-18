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
        GorillaTagger.OnPlayerSpawned(() =>
        {
            manager = new NotificationManager().Setup();
#if DEBUG
            gameObject.AddComponent<Tests.LineSpammer>();
#endif
        });
    }

    public static void Log(string message, LogLevel level = LogLevel.Info)
    {
        Instance.Logger.Log(level, message);
    }

    private void OnEnable()
    {
        NotificationManager.Instance.TextMesh.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        NotificationManager.Instance.TextMesh.gameObject.SetActive(false);
    }
}
