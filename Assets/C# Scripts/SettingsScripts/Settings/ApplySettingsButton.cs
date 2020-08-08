using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplySettingsButton : MonoBehaviour
{
    public SettingsContentBehaviour contentRoot;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        foreach (KeyValuePair<string, object> setting in contentRoot.SettingsToSet)
        {
            switch (setting.Value)
            {
                case Tuple<string, bool, object[]> tuple:
                    SettingsHelper.SetPrefEntry(setting.Key, tuple);
                    break;
                case int i:
                    PlayerPrefs.SetInt(setting.Key, i);
                    break;
                case bool b:
                    SettingsHelper.SetPrefBool(setting.Key, b);
                    break;
                case float f:
                    SettingsHelper.SetPrefSlider(setting.Key, f);
                    break;
            }
        }

        contentRoot.SettingsToSet.Clear();
        GetComponent<Button>().interactable = false;
    }
}
