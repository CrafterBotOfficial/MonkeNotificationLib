using System;

namespace MonkeNotificationLib;

public class Notifier : INotifier
{
    private const float MAX_FADEOUT_TIME = 5f; // seconds

    protected string name;

    public bool UseDynamicTextTimeout = false;

    public Notifier(string name)
    {
        this.name = name;
    }

    public void Message(string message)
    {
        NotificationController.AppendMessage(name, message, color: NotificationController.WHITE, fadeOutDelay: GetTimeout(message.Length));
    }

    public void Warning(string message)
    {
        NotificationController.AppendMessage(name, message, NotificationController.GetColor("warning"), fadeOutDelay: GetTimeout(message.Length));
    }
    public void Error(string message)
    {
        NotificationController.AppendMessage(name, message, NotificationController.GetColor("danger"), fadeOutDelay: GetTimeout(message.Length));
    }

    private float GetTimeout(int textLength)
    {
        if (UseDynamicTextTimeout)
        {
            float calculatedDelay = NotificationController.FADE_DELAY + (textLength * 0.04f); // Todo: Verify if this feels good in game
            return Math.Max(calculatedDelay, MAX_FADEOUT_TIME);
        }
        return NotificationController.FADE_DELAY;
    }
}
