# MonkeNotificationLib
A simple plugin for the game Gorilla Tag that allows mods to quickly show the player information in a console-style UI.

## Documentation
To show a message simply call ``NotificationController.AppendMessage("Message Source", "message content");``. If you wish to use MNL more like BepInEx's logger you can use ``var logger = new Notifier("your mod")`` ``logger.Error("Something bad happening")``.

There are several different ways to make simple messages:
- AppendMessage(string source, string message, bool includeTimeStamp = false, float fadeOutDelay = FADE_DELAY)
- AppendMessage(string source, string message, string color = WHITE, float fadeOutDelay = FADE_DELAY)
- AppendMessage(string message, float fadeOutDelay)
- AppendMessage(string message, string color = WHITE, float fadeOutDelay = FADE_DELAY)
Note: As of 1.1.0 all AppendMessage methods will return nothing as the mod no longer uses different text objects for each notification.

See ``Example/`` for more.

## Legal
This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. © 2021 Another Axiom LLC.
