public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance;
    public Notifier logger;

    private void Start()
    {
        Instance = this;
        logger = new Notifier("Mod Name"); // keep short
        logger.Message("Hello, world!"); // normal
        logger.Error("Error message"); // red
        logger.Warning("Warning message"); // yellow
    }

    public static Notifier GetLogger()
    {
        return Instance.logger;
    }
}
