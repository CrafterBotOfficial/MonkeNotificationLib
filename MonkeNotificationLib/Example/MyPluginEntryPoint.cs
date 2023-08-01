using BepInEx;
using ExitGames.Client.Photon;
using MonkeNotificationLib;
using Photon.Realtime;

namespace Example
{
    [BepInPlugin("crafterbot.notificationlib.example", "Example Notification Plugin", "1.0.0")]
    public class MyPluginEntryPoint : BaseUnityPlugin, IInRoomCallbacks
    {
        private void Start()
        {
            Photon.Pun.PhotonNetwork.AddCallbackTarget(this);
        }

        #region Callbacks
        public void OnPlayerEnteredRoom(Player newPlayer) =>
            NotificationController.AppendMessage("Room Event", $"{newPlayer.NickName} has entered the room.");

        public void OnPlayerLeftRoom(Player otherPlayer)=>
            NotificationController.AppendMessage("Room Event", $"{otherPlayer.NickName} has left the room.");
        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) =>
            NotificationController.AppendMessage("Room Event", $"The room properties have been altered, possible gamemode change attempt?".WrapColor("red"));

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {/*throw new System.NotImplementedException();*/}
        public void OnMasterClientSwitched(Player newMasterClient) {/*throw new System.NotImplementedException();*/ }
        #endregion
    }
}
