#if BEPINEX

using BepInEx;
using BepInEx.Logging;

namespace MonkeNotificationLib;

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
internal class Main : BaseUnityPlugin
{
    public static Main Instance;

    private void Start()
    {
        Instance = this;
        HarmonyLib.Harmony.CreateAndPatchAll(System.Reflection.Assembly.GetExecutingAssembly());
    }

    public void Log(object data, LogLevel level = LogLevel.Info)
    {
        Logger.Log(level, data);
    }

    #region Enable/Disable
    private void OnEnable() { }
    private void OnDisable() { }
    #endregion
}

#elif MELONLOADER

using MelonLoader;
using MonkeNotificationLib;

[assembly: MelonInfo(typeof(MonkeNotificationLib.Main), PluginInfo.Name, PluginInfo.Version, PluginInfo.Author)]
[assembly: MelonColor(106, 0 ,10 ,15)]

namespace MonkeNotificationLib;

internal class Main : MelonMod
{
    public static Main Instance;

    public bool enabled = true; // Not bothering to impliment bc all mods that use it wont work with Melons

    public override void OnInitializeMelon()
    {
        Instance = this;
    }
}

#endif