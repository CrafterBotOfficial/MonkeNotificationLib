using BepInEx;

namespace Example;

[BepInPlugin("crafterbot.notificationlib.example", "Example Notification Plugin", "1.0.1")]
[BepInDependency("crafterbot.notificationlib")]
public class Plugin : BaseUnityPlugin
{
    private Callbacks networkCallbacks;

    private void Start()
    {
        networkCallbacks = new Callbacks();
    }
}