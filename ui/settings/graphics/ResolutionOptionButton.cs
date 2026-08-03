namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BSettings;
using Godot;
using System;
using System.Collections.Generic;


public partial class ResolutionOptionButton : OptionButton
{
    List<int> ResolutionX = new List<int>
    {
        3840, 2560, 1920, 1600, 1280, 900
    };

    List<Vector2I> validWindowSize = new();

    public override void _Ready()
    {
        AddItems();

        Vector2I windowSize = new Vector2I(SettingsManager.Instance.CurrentSettingsData.WindowResolution_X, SettingsManager.Instance.CurrentSettingsData.WindowResolution_Y);
        Selected = validWindowSize.IndexOf(windowSize);

        GetTree().Root.SizeChanged += ToggleEnable;

        ToggleEnable();
    }

    public override void _ExitTree()
    {
        GetTree().Root.SizeChanged -= ToggleEnable;
    }


    private void AddItems()
    {
        int currentDisplayIndex = DisplayServer.WindowGetCurrentScreen();
        Vector2I displaySize = DisplayServer.ScreenGetSize(currentDisplayIndex);

        foreach (int i in ResolutionX)
        {
            if (i <= displaySize.X)
            {
                int displaySize_Y = i * 9 /16;

                if (displaySize_Y <= displaySize.Y)
                {
                    AddItem(i + "X" + displaySize_Y);
                    validWindowSize.Add(new Vector2I(i, displaySize_Y));
                }
            }
        }
    }

    private void _on_item_selected(int index)
    {
        SettingsManager.Instance.ApplyWindowResolution(validWindowSize[index]);
    }

    private void ToggleEnable()
    {
        this.Disabled = !(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed);
    }

}
