using ExitGames.Client.Photon;
using MonkeNotificationLib;
using Photon.Realtime;
using System.Linq;

namespace Example;

public class Callbacks : IInRoomCallbacks, IOnEventCallback
{
    public Callbacks()
    {
        Photon.Pun.PhotonNetwork.AddCallbackTarget(this);
    }

    #region IInRoom Implimentations

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        NotificationController.AppendMessage("Room Event", $"{newPlayer.NickName} has entered the room.");
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        NotificationController.AppendMessage("Room Event", $"{otherPlayer.NickName} has left the room.");
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {/*throw new NotImplementedException();*/}
    public void OnMasterClientSwitched(Player newMasterClient) {/*throw new NotImplementedException();*/}
    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {/*throw new NotImplementedException();*/}
    #endregion

    #region IOnEvent Implimentation
    public void OnEvent(EventData photonEvent)
    {
        if ((photonEvent.Code != 1 && photonEvent.Code != 2) || photonEvent.CustomData is not object[] eventData) return;
        var playerList = Photon.Pun.PhotonNetwork.PlayerList;

        Player tagging = playerList.First(x => x.UserId == (string)eventData[0]);
        Player tagged = playerList.First(x => x.UserId == (string)eventData[1]);
        NotificationController.AppendMessage("Game Event", $"{tagging.NickName} tagged {tagged.NickName}!");
    }
    #endregion
}