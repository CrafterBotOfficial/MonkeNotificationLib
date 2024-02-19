using HarmonyLib;

namespace MonkeNotificationLib.Patches;

[HarmonyPatch(typeof(GorillaTagger), "Start")]
internal static class GorillaTaggerPatch
{
    [HarmonyPostfix]
    private static void Start_Postfix()
    {
        new NotificationManager();
        NotificationController.AppendMessage("MonkeNotificationLib", "Loaded!");
    }
}