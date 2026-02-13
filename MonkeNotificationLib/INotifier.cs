namespace MonkeNotificationLib;

public interface INotifier
{
    public void Message(string message);
    public void Warning(string message);
    public void Error(string message);
}
