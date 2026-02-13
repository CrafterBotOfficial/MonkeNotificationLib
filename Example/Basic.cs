public class YourMod
{
    public void OnJoin(Player newPlayer)
    {
        NotificationController.AppendMessage("Room Event", $"{newPlayer.NickName} has entered the room."); // will display "<bold>[Room Event]</bold> CHIN has entered the room."
    }

    public void OnLeave(Player player)
    {
        NotificationController.AppendMessage($"<b>[Room Event]</b> {player.NickName} has " + "left".WrapColor("danger") + " the room."); // will display "<bold>[Room Event]</bold> CHIN has <red>left</red> the room."
    }
}
