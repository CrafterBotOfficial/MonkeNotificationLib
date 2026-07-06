using BepInEx;
using BepInEx.Logging;

namespace MonkeNotificationLib;

[BepInPlugin("crafterbot.notificationlib", "MonkeNotificationLib", "1.1.1")]
internal class Main : BaseUnityPlugin
{
    private static Main instance;

    private void Start()
    {
        instance = this;
        GorillaTagger.OnPlayerSpawned(() =>
        {
            new UnityEngine.GameObject("notificationlib", typeof(NotificationManager));
#if DEBUG
            gameObject.AddComponent<Tests.LineSpammer>();
#endif
        });
    }

    public static void Log(object message, LogLevel level = LogLevel.Info)
    {
        instance.Logger.Log(level, message);
    }

    private void OnEnable()
    {
        NotificationManager.Instance?.TextMesh?.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        NotificationManager.Instance?.TextMesh?.gameObject.SetActive(false);
    }
}
