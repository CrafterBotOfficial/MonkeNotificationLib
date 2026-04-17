using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace MonkeNotificationLib;

internal class NotificationManager
{
    public static NotificationManager Instance;

    public TextMeshPro TextMesh;

    private int lineKeyCounter;
    private SortedList<int, Line> lines;
    private List<int> orderedKeys;
    private StringBuilder stringBuilder;

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
        orderedKeys = [];
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
        stringBuilder = new StringBuilder();

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

        return this;
    }

    private void Remove(int id)
    {
        if (!lines.ContainsKey(id)) return;
        lines.Remove(id);
    }

    internal void NewLine(string rawText, float lineFadeoutDelay, string color = NotificationController.WHITE)
    {
        Line line;
        lock (lines)
        {
            lineKeyCounter++;
            line = new Line(rawText, color, 0, lineFadeoutDelay);
            lines.Add(lineKeyCounter, line);
        }
        Build();
        Main.Instance.StartCoroutine(FadeLine(line));
    }

    private IEnumerator FadeLine(Line line)
    {
        var lineId = lineKeyCounter;
        yield return new WaitForSeconds(line.LineFadeoutDelay);
        while (line.OpacityIndex < opacity.Length)
        {
            Build();
            line.OpacityIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        Remove(lineId);
        Build();
    }

    private void Build()
    {
        stringBuilder.Clear();
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            var line = lines.Values[i];
            stringBuilder.Append("<color=#").Append(line.Color)
                .Append("><alpha=#").Append(opacity[Mathf.Clamp(line.OpacityIndex, 0, opacity.Length - 1)])
                .Append(">").Append(line.Text).Append("\n</color>");
        }
        TextMesh.text = stringBuilder.ToString();
    }

    private class Line(string text, string color, int opacityIndex, float lineFadeoutDelay)
    {
        public string Text = text;
        public string Color = color;
        public int OpacityIndex = opacityIndex;
        public float LineFadeoutDelay = lineFadeoutDelay;
    }
}
