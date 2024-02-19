using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeNotificationLib;

internal class TextEffect : MonoBehaviour
{
    public float Delay;

    private Text textObject;
    private Color color { get => textObject.color; set => textObject.color = value; }

    private void Start()
    {
        textObject = GetComponent<Text>();
        StartCoroutine(DelayedFade());
    }

    private IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(Delay);

        while (color.a > 0)
        {
            color = new Color(color.r, color.g, color.b, color.a - 0.01f);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
        Destroy(this);
    }
}