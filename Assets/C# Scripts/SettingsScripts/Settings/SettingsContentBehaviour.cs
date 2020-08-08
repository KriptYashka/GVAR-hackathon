using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Config;

public class SettingsContentBehaviour : MonoBehaviour
{
    public Button defaultMode;

    public GameObject dropdownPrefab;
    public GameObject togglePrefab;
    public GameObject percentSliderPrefab;
    public GameObject textPrefab;

    public Button applyButton;

    private Dictionary<string, object> settingsToSet = new Dictionary<string, object>();
    public Dictionary<string, object> SettingsToSet
    {
        get
        {
            applyButton.interactable = true;
            return settingsToSet;
        }
    }

    void Start()
    {
        defaultMode.onClick.Invoke();
    }

    void OnDestroy()
    {
        SettingsHelper.ReloadSettings();
    }

    public void FillContent(string configPath)
    {
        foreach (Transform transform in gameObject.transform)
        {
            Destroy(transform.gameObject);
        }

        if (string.IsNullOrEmpty(configPath)) return;

        float pos = 0;
        foreach (Tuple<string, SettingType, MethodInfo> tuple in menus[configPath])
        {
            GameObject prefab = null;

            switch (tuple.Item2)
            {
                case SettingType.DROPDOWN:
                    prefab = dropdownPrefab;
                    break;
                case SettingType.TOGGLE:
                    prefab = togglePrefab;
                    break;
                case SettingType.PERCENT_SLIDER:
                    prefab = percentSliderPrefab;
                    break;
                case SettingType.TEXT:
                    prefab = textPrefab;
                    break;
            }

            GameObject setting = Instantiate(prefab);
            setting.name = tuple.Item1;

            setting.transform.SetParent(gameObject.transform);
            setting.transform.localPosition = new Vector3(0, -pos, 0);
            setting.transform.localScale = new Vector3(1, 1, 1);
            pos += setting.GetComponent<RectTransform>().rect.height;

            SettingBehaviour behaviour = setting.GetComponent<SettingBehaviour>();
            behaviour.configPath = $"{configPath}/{tuple.Item1}";

            setting.SetActive(true);
        }
    }

    public void EXIT()
    {
        Application.Quit();
    }
}
