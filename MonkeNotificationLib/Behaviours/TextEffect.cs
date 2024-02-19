using System.Collections;
using UnityEngine;

namespace MonkeNotificationLib.Behaviours
{
    internal class TextEffect : MonoBehaviour
    {
        internal float Delay;

        private void OnEnable()
        {
            StartCoroutine(DelayedFade());
        }

        private IEnumerator DelayedFade()
        {
            yield return new WaitForSeconds(Delay);
            NotificationManager.Instance.FadeOutLine(gameObject);
        }
    }
}
