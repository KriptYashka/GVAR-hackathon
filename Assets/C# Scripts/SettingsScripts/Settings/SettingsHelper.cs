using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Config;

public static class SettingsHelper
{
    public static void ApplyResolution(int x, int y) => Screen.SetResolution(x, y, Screen.fullScreen);
    public static void ApplyPreset() { }
    public static void ApplyAntialiasing(int level) => QualitySettings.antiAliasing = level;
    public static void ApplyAnisotropicFiltering(bool isEnabled) => QualitySettings.anisotropicFiltering = (AnisotropicFiltering)Convert.ToInt32(isEnabled);
    public static void ApplyShadowQuality(int level) => QualitySettings.shadows = (ShadowQuality)level;
    public static void ApplyShadowResolution(int level) => QualitySettings.shadowResolution = (ShadowResolution)level;
    public static void ApplyTextureResolution(int level) => QualitySettings.masterTextureLimit = level;
    public static void ApplyModelQuality(float value) { }
    public static void ApplyRenderQuality(float value) { }
    public static void ApplyFullscreen(bool isEnabled) => Screen.fullScreen = Convert.ToBoolean(isEnabled);
    public static void ApplyVsync(bool isEnabled) => QualitySettings.vSyncCount = Convert.ToInt32(isEnabled);

    public static void ReloadSettings()
    {
        foreach (KeyValuePair<string, Tuple<string, SettingType, MethodInfo>[]> menu in menus)
        {
            foreach (Tuple<string, SettingType, MethodInfo> tuple in menu.Value)
            {
                object[] parameters = null;

                switch (tuple.Item2)
                {
                    case SettingType.DROPDOWN:
                        parameters = GetPrefEntry($"{menu.Key}/{tuple.Item1}").Item3;
                        break;
                    case SettingType.PERCENT_SLIDER:
                        parameters = new object[] { GetPrefSlider($"{menu.Key}/{tuple.Item1}") };
                        break;
                    case SettingType.TOGGLE:
                        parameters = new object[] { GetPrefBool($"{menu.Key}/{tuple.Item1}") };
                        break;
                    default:
                        return;
                }

                tuple.Item3.Invoke(null, parameters);
            }
        }
    }

    public static Tuple<string, bool, object[]> GetPrefEntry(string key)
    {
        string current = PlayerPrefs.GetString(key, (string)defaults[key]);
        return entryList[key].Where(x => x.Item1 == current).Single();
    }

    public static int GetPrefInt(string key)
    {
        return PlayerPrefs.GetInt(key, Convert.ToInt32(defaults[key]));
    }

    public static bool GetPrefBool(string key)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaults[key])));
    }

    public static float GetPrefSlider(string key)
    {
        return float.Parse(PlayerPrefs.GetString(key, ((Tuple<int, int, int, bool>)defaults[key]).Item3.ToString()));
    }

    public static void SetPrefEntry(string key, Tuple<string, bool, object[]> value)
    {
        PlayerPrefs.SetString(key, value.Item1);
    }

    public static void SetPrefBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public static void SetPrefSlider(string key, float value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }
}
