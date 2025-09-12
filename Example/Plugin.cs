using BepInEx;
using MonkeNotificationLib;
using Photon.Realtime;

namespace Example;

[BepInPlugin("crafterbot.notificationlib.example", "Example Notification Plugin", "1.0.2")]
[BepInDependency("crafterbot.notificationlib")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        GorillaTagger.OnPlayerSpawned(OnGameInitialized);
    }

    private void OnGameInitialized()
    {
        Logger.LogDebug("Game initialized");
        if (NetworkSystem.Instance is not NetworkSystem networkSystem)
            return;
        networkSystem.OnPlayerJoined += OnPlayerEnteredRoom;
        networkSystem.OnPlayerLeft += OnPlayerLeftRoom;
    }

    public void OnPlayerEnteredRoom(NetPlayer newPlayer)
    {
        NotificationController.AppendMessage("Room Event", $"{newPlayer.SanitizedNickName} has entered the room.");
    }

    public void OnPlayerLeftRoom(NetPlayer otherPlayer)
    {
        NotificationController.AppendMessage("Room Event", $"{otherPlayer.SanitizedNickName} has left the room.");
    }
}
