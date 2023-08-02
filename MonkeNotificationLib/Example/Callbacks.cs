using ExitGames.Client.Photon;
using MonkeNotificationLib;
using Photon.Realtime;
using System.Linq;

namespace Example
{
    internal class Callbacks : IInRoomCallbacks, IOnEventCallback
    {
        internal Callbacks() =>
            Photon.Pun.PhotonNetwork.AddCallbackTarget(this);

        #region IInRoomCallbacks

        void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
        {
            NotificationController.AppendMessage("Room Event", $"{newPlayer.NickName} has entered the room.".WrapColor("green"));
        }

        void IInRoomCallbacks.OnPlayerLeftRoom(Player otherPlayer)
        {
            NotificationController.AppendMessage("Room Event", $"{otherPlayer.NickName} has left the room.".WrapColor("green"));
        }

        void IInRoomCallbacks.OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {/*throw new NotImplementedException();*/}
        void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
        {
            NotificationController.AppendMessage("Player Event", $"{newMasterClient.NickName} is now the master client for the server.".WrapColor("green"));
        }
        void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            NotificationController.AppendMessage("Room Event", "The room properties have been altered, possible gamemode change attempt?".WrapColor("red"));
        }
        #endregion

        #region IOnEventCallback
        void IOnEventCallback.OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == 1 || photonEvent.Code == 2)
            {
                if (photonEvent.CustomData is object[] eventData)
                {
                    var playerList = Photon.Pun.PhotonNetwork.PlayerList;
                    
                    Player tagging = playerList.First(x => x.UserId == (string)eventData[0]);
                    Player tagged = playerList.First(x => x.UserId == (string)eventData[1]);
                    NotificationController.AppendMessage("Game Event", $"{tagging.NickName} tagged {tagged.NickName}!");
                }
            }
        }
        #endregion
    }
}
