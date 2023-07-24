using HarmonyLib;

namespace MonkeNotificationLib
{
    internal static class Patches
    {
        [HarmonyPatch(typeof(GorillaTagger), "Start"), HarmonyPostfix]
        private static void GorillaTagger_Start_Postfix()
        {
            new NotificationManager();
            NotificationController.AppendMessage("MonkeNotificationLib", "Loaded!");
        }
    }
}
