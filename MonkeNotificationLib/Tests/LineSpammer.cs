#if DEBUG

using UnityEngine;

namespace MonkeNotificationLib.Tests;

public class LineSpammer : MonoBehaviour
{
    private Notifier logger;

    private void Awake()
    {
        logger = new Notifier("Mod Menu");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Msg")) logger.Message("Hello world");
        else if (GUILayout.Button("Warning")) logger.Warning("This is a warning");
        else if (GUILayout.Button("Error")) logger.Error("This is an error");
        else if (GUILayout.Button("Long lived")) NotificationController.AppendMessage("Long lived message", 10); // 10 seconds
    }
}

#endif
