namespace BitterCitrus.SRC.Core.BSettings;

using Godot;
using System;
using System.Collections.Generic;

// 설정 값들을 담고 있는 클래스
// SettingsManager 클래스의 CurrentSettingsData 변수에 저장되어 있음.
// SettingsDataRepository 클래스가 settings_data.json 파일에 읽고 씀

// 각 변수의 자료형은 Godot 엔진 내 해당 설정 적용 함수에 들어가는 인자의 형태로.

public class SettingsData
{
    // Gameplay
    public string LanguageLocaleCode { get; set; } = "en";
    public bool EnableScreenShake { get; set; } = true;
    public bool AlwaysShowHUD { get; set; } = false;



    // Graphics
    public int DisplayIndex { get; set; } = 0;
    public DisplayServer.WindowMode WindowMode { get; set; } = DisplayServer.WindowMode.Fullscreen;
    public int WindowResolution_X { get; set; } = 1600;
    public int WindowResolution_Y { get; set; } = 900;
    public DisplayServer.VSyncMode VSyncMode { get; set; } = DisplayServer.VSyncMode.Enabled;
    public int FPSLimit { get; set; } = 0;
    public RenderingServer.ViewportMsaa MSAAMode { get; set; } = RenderingServer.ViewportMsaa.Disabled;
    public float Brightness { get; set; } = 0.5f;



    // Sounds
    public List<float> Volumes { get; set; } = new List<float>
    {
        0.6f,
        0.6f,
        0.6f,
        0.6f,
        0.6f,
    };

    public List<bool> IsMute { get; set; } = new List<bool>
    {
        false,
        false,
        false,
        false,
        false,
    };



    public bool MuteInBackground { get; set; } = false;

    // Controls
    public bool EnableChangeInputModeAutomatically { get; set; } = true;
    public bool UseNintendoLayout { get; set; } = false;
    public bool EnableControllerVibration { get; set; } = false;
}

public enum InputType { JoyButton, JoyAxis, Key, MouseButton }

public struct InputData
{
    public InputType Type { get; set; }
    public int Id { get; set; }

    public InputData(JoyButton button) { Type = InputType.JoyButton; Id = (int)button; }
    public InputData(JoyAxis axis) { Type = InputType.JoyAxis; Id = (int)axis; }
    public InputData(Key key) { Type = InputType.Key; Id = (int)key; }
    public InputData(MouseButton button) { Type = InputType.MouseButton; Id = (int)button; }
}