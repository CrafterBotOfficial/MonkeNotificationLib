using MelonLoader;

[assembly: MelonInfo(typeof(MonkeNotificationLib.Main), "MonkeNotificationLib", "1.0.2", "Crafterbot")]
[assembly: MelonGame("Another Axiom", "Gorilla Tag")]

namespace MonkeNotificationLib;

internal class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        GorillaTagger.OnPlayerSpawned(Load);
    }

    private void Load()
    {
        new NotificationManager();
        NotificationController.AppendMessage("MonkeNotificationLib", "Loaded!");
    }
}
