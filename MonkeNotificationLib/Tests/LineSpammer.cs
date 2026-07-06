#if DEBUG

using UnityEngine;

namespace MonkeNotificationLib.Tests;

public class LineSpammer : MonoBehaviour
{
    private Notifier logger;

    private bool spam;
    private bool spamLong;
    private bool numberedSpam;
    private int numberedSpamCount;

    private void Awake()
    {
        logger = new Notifier("Log Source");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Msg")) logger.Message("Hello world");
        else if (GUILayout.Button("Warning")) logger.Warning("This is a warning");
        else if (GUILayout.Button("Error")) logger.Error("This is an error");
        else if (GUILayout.Button("Long lived")) NotificationController.AppendMessage("Long lived message", 10); // 10 seconds

        spam = GUILayout.Toggle(spam, "Spam");
        if (spam) logger.Message("Spam");

        spamLong = GUILayout.Toggle(spamLong, "Spam Longer");
        if (spamLong)
            logger.Message("""
                    NullReferenceException: Object reference not set to an instance of an object
                    Stack trace:
                    BuilderZoneRenderers.OnZoneChanged () (at <fc6c8fa138054a3f9bf20d57b3a15580>:0)
                    BuilderZoneRenderers.Start () (at <fc6c8fa138054a3f9bf20d57b3a15580>:0)");
        """);

        numberedSpam = GUILayout.Toggle(numberedSpam, "Spam but numbered");
        if (numberedSpam)
        {
            numberedSpamCount++;
            logger.Message(numberedSpamCount.ToString());
        }

        GUILayout.Label($"Characters: " + NotificationManager.Instance?.TextMesh?.text?.Length);
    }
}

#endif
