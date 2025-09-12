# MonkeNotificationLib
A simple plugin for the game Gorilla Tag that allows mods to quickly show the player information in a console-style UI.

## Documentation
There are only two public methods in the entire mod, making it very easy to use. To append a message simply call ``UnityEngine.UI.Text line = NotificationController.AppendMessage("Message Source", "message content");``. This will pull a line from the object pool and display it to the user. Note if the object pool runs out it will create a new line, however it would be inefficent and lag the game.
Needs custom colors? There is also a extension method for quickly wrapping your string in color. Note there are a couple colors that will be overriden for my own colors, check the source code. ``"My string".WrapColor("red")``

## Legal
This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. © 2021 Another Axiom LLC.
