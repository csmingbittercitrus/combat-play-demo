namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BSettings;
using Godot;
using System;

public partial class FullscreenCheckbox : CheckBox
{
    public override void _Ready()
    {
        ButtonPressed = (SettingsManager.Instance.CurrentSettingsData.WindowMode == DisplayServer.WindowMode.Fullscreen);
    }

    private void _on_toggled(bool enable)
    {
        if (enable)
        {
            SettingsManager.Instance.ApplyWindowMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            SettingsManager.Instance.ApplyWindowMode(DisplayServer.WindowMode.Windowed);
        }
    }
}
