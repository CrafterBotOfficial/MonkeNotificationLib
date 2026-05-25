namespace MonkeNotificationLib;

public class Notifier(string name) : INotifier
{
    private const float MAX_FADEOUT_TIME = 5f; // seconds

    public bool UseDynamicTextTimeout;

    public virtual void Debug(string message)
    {
        NotificationController.AppendMessage(name, message, color: NotificationController.GetColor("gray"), fadeOutDelay: GetTimeout(message.Length));
    }

    public virtual void Message(string message)
    {
        NotificationController.AppendMessage(name, message, color: NotificationController.WHITE, fadeOutDelay: GetTimeout(message.Length));
    }

    public virtual void Warning(string message)
    {
        NotificationController.AppendMessage(name, message, NotificationController.GetColor("warning"), fadeOutDelay: GetTimeout(message.Length));
    }
    public virtual void Error(string message)
    {
        NotificationController.AppendMessage(name, message, NotificationController.GetColor("danger"), fadeOutDelay: GetTimeout(message.Length));
    }

    public virtual float GetTimeout(int textLength)
    {
        if (UseDynamicTextTimeout)
        {
            float calculatedDelay = NotificationController.FADE_DELAY + (textLength * 0.04f); // Todo: Verify if this feels good in game
            return System.Math.Max(calculatedDelay, MAX_FADEOUT_TIME);
        }
        return NotificationController.FADE_DELAY;
    }
}
