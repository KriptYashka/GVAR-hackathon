using System;
using System.Collections.Generic;
using System.Reflection;

// You should load these settings from somewhere
public static class Config
{
    public enum SettingType
    {
        DROPDOWN,
        TOGGLE,
        PERCENT_SLIDER,
        TEXT,
    }

    public static Dictionary<string, Tuple<string, SettingType, MethodInfo>[]> menus = new Dictionary<string, Tuple<string, SettingType, MethodInfo>[]>
    {
        { "video", new Tuple<string, SettingType, MethodInfo>[]
        {
            //new Tuple<string, SettingType>("restart_warning", SettingType.TEXT),
            new Tuple<string, SettingType, MethodInfo>("resolution", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyResolution")),
            new Tuple<string, SettingType, MethodInfo>("preset", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyPreset")),
            new Tuple<string, SettingType, MethodInfo>("smoothing", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyAntialiasing")),
            new Tuple<string, SettingType, MethodInfo>("aniso_filtering", SettingType.TOGGLE, typeof(SettingsHelper).GetMethod("ApplyAnisotropicFiltering")),
            new Tuple<string, SettingType, MethodInfo>("shadow_quality", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyShadowQuality")),
            new Tuple<string, SettingType, MethodInfo>("shadow_resolution", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyShadowResolution")),
            new Tuple<string, SettingType, MethodInfo>("texture_resolution", SettingType.DROPDOWN, typeof(SettingsHelper).GetMethod("ApplyTextureResolution")),
            new Tuple<string, SettingType, MethodInfo>("model_quality", SettingType.PERCENT_SLIDER, typeof(SettingsHelper).GetMethod("ApplyModelQuality")),
            new Tuple<string, SettingType, MethodInfo>("render_quality", SettingType.PERCENT_SLIDER, typeof(SettingsHelper).GetMethod("ApplyRenderQuality")),
            new Tuple<string, SettingType, MethodInfo>("fullscreen", SettingType.TOGGLE, typeof(SettingsHelper).GetMethod("ApplyFullscreen")),
            new Tuple<string, SettingType, MethodInfo>("vsync", SettingType.TOGGLE, typeof(SettingsHelper).GetMethod("ApplyVsync")),
        }},
    };

    public static Dictionary<string, Tuple<string, bool, object[]>[]> entryList = new Dictionary<string, Tuple<string, bool, object[]>[]>
    {
        { "video/resolution", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("1280x720",  false, new object[] { 1280, 720 }),
            new Tuple<string, bool, object[]>("1366x768",  false, new object[] { 1366, 768 }),
            new Tuple<string, bool, object[]>("1920x1080", true,  new object[] { 1920, 1080 }),
            new Tuple<string, bool, object[]>("2560x1440", false, new object[] { 2560, 1440 }),
        }},
        { "video/preset", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("preset1", true,  new object[] { }),
            new Tuple<string, bool, object[]>("preset2", false, new object[] { }),
            new Tuple<string, bool, object[]>("preset3", false, new object[] { }),
        }},
        { "video/smoothing", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("Disabled", true,  new object[] { 0 }),
            new Tuple<string, bool, object[]>("2x",       false, new object[] { 2 }),
            new Tuple<string, bool, object[]>("4x",       false, new object[] { 4 }),
            new Tuple<string, bool, object[]>("8x",       false, new object[] { 8 }),
            new Tuple<string, bool, object[]>("16x",      false, new object[] { 16 }),
        }},
        { "video/shadow_quality", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("Disabled", true,  new object[] { 0 }),
            new Tuple<string, bool, object[]>("Low",      false, new object[] { 1 }),
            new Tuple<string, bool, object[]>("High",     false, new object[] { 2 }),
        }},
        { "video/shadow_resolution", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("Low",    true,  new object[] { 0 }),
            new Tuple<string, bool, object[]>("Medium", false, new object[] { 1 }),
            new Tuple<string, bool, object[]>("High",   false, new object[] { 2 }),
        }},
        { "video/texture_resolution", new Tuple<string, bool, object[]>[]
        {
            new Tuple<string, bool, object[]>("Low",    true,  new object[] { 2 }),
            new Tuple<string, bool, object[]>("Medium", false, new object[] { 1 }),
            new Tuple<string, bool, object[]>("High",   false, new object[] { 0 }),
        }},
    };

    public static Dictionary<string, object> defaults = new Dictionary<string, object>
    {
        { "video/resolution", "1920x1080" },
        { "video/preset", "preset1" },
        { "video/smoothing", "16x" },
        { "video/aniso_filtering", true },
        { "video/shadow_quality", "High" },
        { "video/shadow_resolution", "High" },
        { "video/texture_resolution", "High" },
        { "video/model_quality", new Tuple<int, int, int, bool>(1, 4, 3, true) }, // tuple<minValue, maxValue, value, showPercents>
        { "video/render_quality", new Tuple<int, int, int, bool>(25, 100, 100, true) },
        { "video/fullscreen", true },
        { "video/vsync", false },
    };

    public static Dictionary<string, string> labels = new Dictionary<string, string>
    {
        { "video/resolution", "Разрешение экрана:" },
        { "video/preset", "Пресеты качества:" },
        { "video/smoothing", "Сглаживание:" },
        { "video/aniso_filtering", "Анизотропная фильтрация" },
        { "video/shadow_quality", "Качество теней:" },
        { "video/shadow_resolution", "Разрешение теней:" },
        { "video/texture_resolution", "Разрешение текстур:" },
        { "video/model_quality", "Качество моделей:" },
        { "video/render_quality", "Качество рендера:" },
        { "video/fullscreen", "Fullscreen" },
        { "video/vsync", "V-Sync" },
        { "video/restart_warning", "При нажатии кнопки \"Применить\" игра будет перезапущена." }
    };

    public static Dictionary<string, string> translations = new Dictionary<string, string>
    {
        { "Disabled", "Отключено" },
        { "Low", "Низкое" },
        { "Medium", "Среднее" },
        { "High", "Высокое" }
    };
}
