/*
    A very inefficent example, but you can see how easy it is to spawn a notification.
*/
using BepInEx;
using MonkeNotificationLib;
using UnityEngine.XR;

namespace Example
{
    [BepInPlugin("crafterbot.notificationlib.example", "Example Notification Plugin", "1.0.0")]
    public class MyPluginEntryPoint : BaseUnityPlugin
    {
        public static MyPluginEntryPoint Instance;

        private bool _triggerOneShot;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (UniverseLib.Input.InputManager.GetKeyDown(UnityEngine.KeyCode.T))
            {
                NotificationController.AppendMessage("Example", "You pressed T!", true);
            }

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out float value);
            if (value > 0.5f)
            {
                if (!_triggerOneShot)
                {
                    _triggerOneShot = true;
                    NotificationController.AppendMessage("Criticial Error".WrapColor("red"), "You pressed your grip :P", true);
                }
                return;
            }
            _triggerOneShot = false;
        }
    }
}
