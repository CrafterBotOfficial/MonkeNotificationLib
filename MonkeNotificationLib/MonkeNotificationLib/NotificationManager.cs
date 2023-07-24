using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeNotificationLib
{
    internal class NotificationManager
    {
        internal static NotificationManager Instance;
        private static bool _initialized;

        internal GameObject ConsoleCanvasObject;
        internal GameObject ConsoleLinePrefab;

        private int _availableLines { get => _linePool.Count(x => !x.gameObject.activeSelf); }
        private List<Text> _linePool = new List<Text>();

        internal NotificationManager()
        {
            Instance = this;
            using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeNotificationLib.Resources.console"))
            {
                AssetBundle assetBundle = AssetBundle.LoadFromStream(stream);

                ConsoleCanvasObject = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>("ConsoleCanvas"));
                ConsoleCanvasObject.transform.SetParent(GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/head/").transform);
                ConsoleCanvasObject.transform.localPosition = new Vector3(-0.816f, -0.057f, 1.304f);
                ConsoleCanvasObject.transform.localRotation = Quaternion.Euler(-0.816f, -0.057f, 1.304f); // Quaternion.Euler(-7.609f, 0, 0);

                ConsoleLinePrefab = ConsoleCanvasObject.transform.GetChild(0).gameObject;
                ConsoleLinePrefab.SetActive(false);

                const int linePoolAmount = 350;
                for (int i = 0; i < linePoolAmount; i++)
                {
                    var newLine = GameObject.Instantiate(ConsoleLinePrefab, ConsoleCanvasObject.transform);
                    _linePool.Add(newLine.GetComponent<Text>());
                    _linePool[i].enabled = true;
                }
                assetBundle.Unload(false);
            }
            _initialized = true;
        }

        internal void NewLine(string text)
        {
            if (!_initialized || !Main.Instance.enabled)
                return;
            if (_availableLines == 0)
            {
                Main.Log("No objects to pull from the pool, manually increasing pool size. current pool size:" + _linePool.Count, BepInEx.Logging.LogLevel.Warning);
                _linePool.Add(GameObject.Instantiate(ConsoleLinePrefab, ConsoleCanvasObject.transform).GetComponent<Text>());
            }
            Text newLine = _linePool.First(x => !x.gameObject.activeSelf);
            newLine.text = text;
            newLine.color = Color.white;
            newLine.gameObject.AddComponent<Behaviours.TextEffect>().Delay = 3;
            newLine.gameObject.SetActive(true);

            // force fix for the font having missing characters and the text being reset to the default font
        }

        internal async void FadeOutLine(GameObject line)
        {
            var lineText = line.GetComponent<Text>();
            while (lineText.color.a > 0)
            {
                lineText.color = new Color(lineText.color.r, lineText.color.g, lineText.color.b, lineText.color.a - 0.01f);
                await System.Threading.Tasks.Task.Delay(10);
            }
            Object.Destroy(line.GetComponent<Behaviours.TextEffect>());
            line.SetActive(false);

            line.transform.SetAsFirstSibling();
        }
    }
}
