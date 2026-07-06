using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace MonkeNotificationLib;

internal class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    public const int MAX_VISIBLE_LINES = 45;

    public TextMeshPro TextMesh;

    private List<Line> lines;
    private StringBuilder stringBuilder;

    private string[] opacity;

    private void Start()
    {
        if (Instance is not null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        lines = [];
        opacity = ["FF", "EE", "DD", "CC", "BB", "AA", "99", "88", "77", "66", "55", "44", "33", "22", "11", "00",];
        stringBuilder = new StringBuilder(40_000);

        var container = new GameObject().transform;
        container.SetParent(VRRigCache.Instance.localRig.transform.Find("rig/head"));
        container.localScale = Vector3.one * .0175f;
        container.localPosition = new Vector3(-0.35f, -1.1f, 1.904f);
        container.localRotation = Quaternion.Euler(-0.816f, -0.057f, 1.304f);

        TextMesh = container.AddComponent<TextMeshPro>();
        TextMesh.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name == "UtopiumPixel SDF"); // Todo: extract old font from assetbundle
        TextMesh.fontSize = 26;
        TextMesh.rectTransform.sizeDelta = new Vector2(100f, 100f);
        TextMesh.richText = true;
        TextMesh.fontMaterial.shader = Shader.Find("GUI/Text Shader");
        TextMesh.maxVisibleCharacters = 10_000;

        Loop().ContinueWith(task =>
        {
            Main.Log(task.Exception, BepInEx.Logging.LogLevel.Error);
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    private async Task Loop()
    {
        while (true)
        {
            if (lines.Count != 0) Build();
            await Task.Delay(150);
        }
    }

    internal void NewLine(string rawText, float lineFadeoutDelay, string color = NotificationController.WHITE)
    {
        Line line;
        lock (lines)
        {
            line = new Line(rawText, color, 0, lineFadeoutDelay);
            lines.Insert(0, line);
        }
        Build();
        StartCoroutine(FadeLine(line));
    }

    private IEnumerator FadeLine(Line line)
    {
        yield return new WaitForSeconds(line.LineFadeoutDelay);
        while (line.OpacityIndex < opacity.Length)
        {
            line.OpacityIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        lines.Remove(line);
    }

    private void Build()
    {
        stringBuilder.Clear();
        int depth = Mathf.Clamp(lines.Count, 0, MAX_VISIBLE_LINES) - 1;
        for (int i = 0; i <= depth; i++)
        {
            var line = lines[i];
            stringBuilder
                .Append("<color=#").Append(line.Color)
                .Append("><alpha=#").Append(opacity[Mathf.Clamp(line.OpacityIndex, 0, opacity.Length - 1)])
                .Append(">").Append(line.Text).Append("\n</color>");
        }

        TextMesh.SetText(stringBuilder.ToString());
    }

    private class Line(string text, string color, int opacityIndex, float lineFadeoutDelay)
    {
        public string Text = text;
        public string Color = color;
        public int OpacityIndex = opacityIndex;
        public float LineFadeoutDelay = lineFadeoutDelay;
    }
}
