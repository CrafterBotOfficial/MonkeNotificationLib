using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MonkeNotificationLib;

internal class NotificationManager
{
    public static NotificationManager Instance;

    public TextMeshPro TextMesh;

    private volatile int lineKeyCounter;
    private ObservableDictionary<int, Line> lines;

    private string[] opacity;

    internal NotificationManager Setup()
    {
        if (Instance is not null)
        {
            // throw new System.Exception("Cannot instantiate multiple NotificationManagers. Use NotificationManager.Instance instead.");
            return Instance;
        }
        Instance = this;

        lines = [];
        lines.CollectionChanged += (_, _) => Build();
        opacity = [
            "FF",
            "EE",
            "DD",
            "CC",
            "BB",
            "AA",
            "99",
            "88",
            "77",
            "66",
            "55",
            "44",
            "33",
            "22",
            "11",
            "00",
        ];

        var container = new GameObject().transform;
        container.SetParent(VRRigCache.Instance.localRig.transform.Find("rig/head"));
        container.localScale = Vector3.one * .0175f;
        container.localPosition = new Vector3(-0.55f, -0.3f, 1.604f);
        container.localRotation = Quaternion.Euler(-0.816f, -0.057f, 1.304f);

        TextMesh = container.AddComponent<TextMeshPro>();
        TextMesh.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name == "UtopiumPixel SDF"); // Todo: extract old font from assetbundle
        TextMesh.rectTransform.sizeDelta = new Vector2(100f, 100f);
        TextMesh.richText = true;
        TextMesh.fontMaterial.shader = Shader.Find("GUI/Text Shader");

        return this;
    }

    private void Remove(int id)
    {
        if (!lines.ContainsKey(id)) return;
        lines.Remove(id);
    }

    internal void NewLine(string rawText, float lineFadeoutDelay, string color = NotificationController.WHITE)
    {
        lineKeyCounter++;
        var line = new Line(rawText, color, 0, lineFadeoutDelay);
        lines.Insert(lineKeyCounter, line);
        Main.Instance.StartCoroutine(FadeLine(line));
    }

    private IEnumerator FadeLine(Line line)
    {
        var lineId = lineKeyCounter;
        yield return new WaitForSeconds(line.LineFadeoutDelay);
        while (line.OpacityIndex < opacity.Length)
        {
            Main.Log($"{line.OpacityIndex}/{opacity.Length}");
            Build();
            line.OpacityIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        lock (lines)
        {
            Remove(lineId);
        }
    }

    private void Build()
    {
        var text = string.Empty;
        foreach (var line in lines) 
            text += $"<color=#{line.Value.Color}><alpha=#{opacity[Mathf.Clamp(line.Value.OpacityIndex, 0, opacity.Length - 1)]}>{line.Value.Text}\n</color>";
        TextMesh.text = text;
    }

    private class Line(string text, string color, int opacityIndex, float lineFadeoutDelay)
    {
        public string Text = text;
        public string Color = color;
        public int OpacityIndex = opacityIndex;
        public float LineFadeoutDelay = lineFadeoutDelay;
    }
}
