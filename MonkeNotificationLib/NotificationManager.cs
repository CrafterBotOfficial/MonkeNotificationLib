using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeNotificationLib;

internal class NotificationManager
{
    public static NotificationManager Instance;
    private static bool initialized;

    public GameObject ConsoleCanvasObject;
    public GameObject ConsoleLinePrefab;

    private int availableLines => linePool.Count(x => !x.gameObject.activeSelf); 
    private List<Text> linePool = new List<Text>();

    public NotificationManager()
    {
        Instance = this;
        Main.Log("Initializing notification manager", BepInEx.Logging.LogLevel.Message);
        using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeNotificationLib.Resources.console"))
        {
            AssetBundle assetBundle = AssetBundle.LoadFromStream(stream);

            ConsoleCanvasObject = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>("ConsoleCanvas"));
            Transform consoleTransform = ConsoleCanvasObject.transform;
            consoleTransform.SetParent(GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/head/").transform);
            consoleTransform.localPosition = new Vector3(-0.816f, -0.157f, 1.604f);
            consoleTransform.localRotation = Quaternion.Euler(-0.816f, -0.057f, 1.304f);

            ConsoleLinePrefab = consoleTransform.GetChild(0).gameObject;
            Text prefabText = ConsoleLinePrefab.GetComponent<Text>();
            Material newMaterial = GameObject.Instantiate(prefabText.material);
            newMaterial.shader = Shader.Find("GUI/Text Shader");
            prefabText.material = newMaterial;

            ConsoleLinePrefab.SetActive(false);

            const int linePoolAmount = 750;
            for (int i = 0; i < linePoolAmount; i++) AddLineToPool();
            assetBundle.Unload(false);
        }
        initialized = true;
    }

    private void AddLineToPool()
    {
        var newLine = GameObject.Instantiate(ConsoleLinePrefab, ConsoleCanvasObject.transform);
        linePool.Add(newLine.GetComponent<Text>());
    }

    public Text NewLine(string text, float fadeOutDelay = 3)
    {
        if (!initialized || !Main.Instance.enabled) return null;
        if (availableLines == 0)
        {
            Main.Log("No objects to pull from the pool, manually increasing pool size. current pool size:" + linePool.Count, BepInEx.Logging.LogLevel.Warning);
            AddLineToPool();
        }

        Text newLine = linePool.First(x => !x.gameObject.activeSelf);
        newLine.text = text;
        newLine.color = Color.white;
        GameObject newLineObject = newLine.gameObject;
        newLineObject.AddComponent<TextEffect>().Delay = fadeOutDelay;
        newLineObject.SetActive(true);
        newLineObject.transform.SetAsFirstSibling();

        return newLine;
    }
}