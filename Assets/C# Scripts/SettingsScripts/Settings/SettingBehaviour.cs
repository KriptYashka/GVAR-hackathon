using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Config;

public class SettingBehaviour : MonoBehaviour
{
    public string configPath;

    public Text label;
    public GameObject settingObject;

    // Start is called before the first frame update
    void Start()
    {
        Text labelText = label.GetComponent<Text>();
        labelText.text = labels[configPath];

        if (settingObject == null) return;

        Dropdown dropdown = settingObject.GetComponent<Dropdown>();
        if (dropdown != null)
        {
            List<Dropdown.OptionData> content = entryList[configPath].Select(entry => new Dropdown.OptionData(translations.TryGetValue(entry.Item1, out string translatedEntry) ? translatedEntry : entry.Item1)).ToList();
            dropdown.AddOptions(content);

            var currentEntry = SettingsHelper.GetPrefEntry(configPath);
            dropdown.value = Array.IndexOf(entryList[configPath], currentEntry);

            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            return;
        }

        Toggle toggle = settingObject.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.isOn = SettingsHelper.GetPrefBool(configPath);
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            return;
        }

        Slider slider = settingObject.GetComponent<Slider>();
        if (slider != null)
        {
            Tuple<int, int, int, bool> tuple = (Tuple<int, int, int, bool>)defaults[configPath];
            slider.minValue = tuple.Item1;
            slider.maxValue = tuple.Item2;
            slider.value = SettingsHelper.GetPrefSlider(configPath);
            slider.onValueChanged.AddListener(OnSliderValueChanged);

            SliderDisplay display = settingObject.GetComponent<SliderDisplay>();
            display.showPercents = tuple.Item4;

            return;
        }

        throw new MissingComponentException();
    }

    // methodInfo: 
    //string[] path = configPath.Split('/');
    //menus[path[0]].Where(x => x.Item1 == path[1]).Single().Item3
    //setting.Value.Invoke(null, setting.Value.Item2);

    public void OnDropdownValueChanged(int value)
    {
        string[] path = configPath.Split('/');
        menus[path[0]].Where(x => x.Item1 == path[1]).Single().Item3.Invoke(null, entryList[configPath][value].Item3);
        GetComponentInParent<SettingsContentBehaviour>().SettingsToSet[configPath] = entryList[configPath][value];
    }

    public void OnToggleValueChanged(bool value)
    {
        string[] path = configPath.Split('/');
        menus[path[0]].Where(x => x.Item1 == path[1]).Single().Item3.Invoke(null, new object[] { value });
        GetComponentInParent<SettingsContentBehaviour>().SettingsToSet[configPath] = value;
    }

    public void OnSliderValueChanged(float value)
    {
        string[] path = configPath.Split('/');
        menus[path[0]].Where(x => x.Item1 == path[1]).Single().Item3.Invoke(null, new object[] { value });
        GetComponentInParent<SettingsContentBehaviour>().SettingsToSet[configPath] = value;
    }
}
