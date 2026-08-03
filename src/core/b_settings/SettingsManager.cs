namespace BitterCitrus.SRC.Core.BSettings;

using Godot;
using System;
using System.Collections.Generic;
using BitterCitrus.SRC.Core.BInput;

// Settings 관련 변수와 함수를 다루는 싱글톤

public partial class SettingsManager : Node
{
    public static SettingsManager Instance;

    public SettingsData CurrentSettingsData;
    public Dictionary<StringName, Key> KeyboardInputMap;
    public Dictionary<StringName, InputData> ControllerInputMap;

    public override void _EnterTree()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void _Ready()
    {
        Init();
        ApplyAllSettings();
    }

    private void Init()
    {
        GD.Print("[SettingsManager] 초기화.");

        CurrentSettingsData = SettingsDataRepository.LoadSettingsData();

        if (CurrentSettingsData == null)
        {
            CurrentSettingsData = new();
            SettingsDataRepository.SaveSettingsData();
        }

        GD.Print("[SettingsManager] KeyboardInputMap 초기화.");

        KeyboardInputMap = SettingsDataRepository.LoadKeyboardInputMap();

        if (KeyboardInputMap == null)
        {
            KeyboardInputMap = InputBinder_Utils.GetNewKeyboardBindings();
            SettingsDataRepository.SaveKeyboardInputMap();
        }


        GD.Print("[SettingsManager] ControllerInputMap 초기화.");

        ControllerInputMap = SettingsDataRepository.LoadControllerInputMap();

        if (ControllerInputMap == null)
        {
            ControllerInputMap = InputBinder_Utils.GetNewControllerBindings();
            SettingsDataRepository.SaveControllerInputMap();
        }
    }

    private void ApplyAllSettings()
    {
        ApplyLanguage(CurrentSettingsData.LanguageLocaleCode);

        ApplyWindowMode(CurrentSettingsData.WindowMode);
        ApplyWindowResolution(new Vector2I(CurrentSettingsData.WindowResolution_X, CurrentSettingsData.WindowResolution_Y));
        ApplyVSyncMode(CurrentSettingsData.VSyncMode);
        ApplyFPSLimit(CurrentSettingsData.FPSLimit);
        ApplyMSAAMode(CurrentSettingsData.MSAAMode);

        for(int i = 0; i < 5; i++)
        {
            ApplyVolume(i, CurrentSettingsData.Volumes[i]);
            ApplyMute(i, CurrentSettingsData.IsMute[i]);
        }
    }



    #region Gameplay
    public void ApplyLanguage(string localeCode)
    {
        CurrentSettingsData.LanguageLocaleCode = localeCode;
        TranslationServer.SetLocale(localeCode);
    }

    public void ApplyScreenShake(bool enable)
    {
        CurrentSettingsData.EnableScreenShake = enable;
    }

    public void ApplyAlwaysShowHUD(bool enable)
    {
        CurrentSettingsData.AlwaysShowHUD = enable;
    }
    #endregion



    #region Graphics
    public void ApplyWindowMode(DisplayServer.WindowMode mode)
    {
        CurrentSettingsData.WindowMode = mode;
        DisplayServer.WindowSetMode(mode);
    }

    public void ApplyWindowResolution(Vector2I resolution)
    {
        CurrentSettingsData.WindowResolution_X = resolution.X;
        CurrentSettingsData.WindowResolution_Y = resolution.Y;
        if (DisplayServer.WindowGetMode() != DisplayServer.WindowMode.Windowed) return;

        DisplayServer.WindowSetSize(resolution);
        AlignWindowToCenter();
    }

    public void ApplyVSyncMode(DisplayServer.VSyncMode mode)
    {
        CurrentSettingsData.VSyncMode = mode;
        DisplayServer.WindowSetVsyncMode(mode);
    }

    public void ApplyFPSLimit(int fps)
    {
        CurrentSettingsData.FPSLimit = fps;
        Engine.MaxFps = fps;
    }

    public void ApplyMSAAMode(RenderingServer.ViewportMsaa mode)
    {
        CurrentSettingsData.MSAAMode = mode;
        Rid mainViewport = GetTree().Root.GetViewportRid();
        RenderingServer.ViewportSetMsaa2D(mainViewport, mode);
    }

    public void ApplyBrightness(float value)
    {
        CurrentSettingsData.Brightness = value;
    }
    #endregion
    
    
    
    #region Sounds
    public void ApplyVolume(int busIndex, float volume)
    {
        CurrentSettingsData.Volumes[busIndex] = volume;
        float db = Mathf.LinearToDb(volume);
        AudioServer.SetBusVolumeDb(busIndex, db);
    }

    public void ApplyMute(int busIndex, bool enable)
    {
        CurrentSettingsData.IsMute[busIndex] = enable;
        AudioServer.SetBusMute(busIndex, enable);
    }

    public void ApplyMuteInBackground(bool enable)
    {
        CurrentSettingsData.MuteInBackground = enable;
    }
    #endregion



    #region Controls
    public void ApplyEnableAutomaticallyChangeInputMode(bool enable)
    {
        CurrentSettingsData.EnableChangeInputModeAutomatically = enable;
    }

    public void ApplyNintendoLayout(bool enable)
    {
        CurrentSettingsData.UseNintendoLayout = enable;
        InputBinder_Controller.ApplyNintendoLayout();
    }

    public void ApplyControllerVibration(bool enable)
    {
        CurrentSettingsData.EnableControllerVibration = enable;
    }
    #endregion



    #region Utils
    private void AlignWindowToCenter()
    {
        if (DisplayServer.WindowGetMode() != DisplayServer.WindowMode.Windowed)
        {
            return;
        }

        CurrentSettingsData.DisplayIndex = DisplayServer.WindowGetCurrentScreen();

        Vector2I displayPosition = DisplayServer.ScreenGetPosition(CurrentSettingsData.DisplayIndex);
        Vector2I displaySize = DisplayServer.ScreenGetSize(CurrentSettingsData.DisplayIndex);
        Vector2I windowSize = DisplayServer.WindowGetSize(0);

        Vector2I targetPosition = displayPosition + ((displaySize - windowSize) / 2);

        DisplayServer.WindowSetPosition(targetPosition);
    }
    #endregion
}
