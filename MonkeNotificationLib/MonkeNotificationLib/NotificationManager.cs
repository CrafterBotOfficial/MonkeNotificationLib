﻿using System.Collections.Generic;
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
                ConsoleCanvasObject.transform.SetParent(GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/head/").transform);
                ConsoleCanvasObject.transform.localPosition = new Vector3(-0.816f, -0.157f, 1.604f);// new Vector3(-0.816f, -0.057f, 1.304f);
                ConsoleCanvasObject.transform.localRotation = Quaternion.Euler(-0.816f, -0.057f, 1.304f); // Quaternion.Euler(-7.609f, 0, 0);

                ConsoleLinePrefab = ConsoleCanvasObject.transform.GetChild(0).gameObject;
                Text prefabText = ConsoleLinePrefab.GetComponent<Text>();
                Material newMaterial = GameObject.Instantiate(prefabText.material);
                newMaterial.shader = Shader.Find("GUI/Text Shader");
                prefabText.material = newMaterial;

                ConsoleLinePrefab.SetActive(false);

                const int linePoolAmount = 550; // Object pool is very high bc I dont know what ppl will be spamming
                for (int i = 0; i < linePoolAmount; i++)
                    AddLineToPool();
                assetBundle.Unload(false);
            }
            _initialized = true;
        }

        private void AddLineToPool()
        {
            var newLine = GameObject.Instantiate(ConsoleLinePrefab, ConsoleCanvasObject.transform);
            _linePool.Add(newLine.GetComponent<Text>());
            // _linePool.Last().enabled = true;
        }

        internal Text NewLine(string text, float fadeOutDelay = 3)
        {
            if (!_initialized || !Main.Instance.enabled)
                return null;
            if (_availableLines == 0)
            {
                Main.Log("No objects to pull from the pool, manually increasing pool size. current pool size:" + _linePool.Count, BepInEx.Logging.LogLevel.Warning);
                AddLineToPool();
            }

            Text newLine = _linePool.First(x => !x.gameObject.activeSelf);
            newLine.text = text;
            newLine.color = Color.white;
            GameObject newLineObject = newLine.gameObject;
            newLineObject.AddComponent<Behaviours.TextEffect>().Delay = fadeOutDelay;
            newLineObject.SetActive(true);
            newLineObject.transform.SetAsFirstSibling();

            return newLine;
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
        }
    }
}
