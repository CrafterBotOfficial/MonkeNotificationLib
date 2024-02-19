using BepInEx;

namespace Example
{
    [BepInPlugin("crafterbot.notificationlib.example", "Example Notification Plugin", "1.0.1")]
    public class MyPluginEntryPoint : BaseUnityPlugin
    {
        private void Start()
        {
            new Callbacks();
            // new HarmonyLib.Harmony(Info.Metadata.GUID).PatchAll(typeof(Patches));
        }
    }
}
