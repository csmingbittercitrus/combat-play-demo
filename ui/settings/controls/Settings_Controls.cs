namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Settings_Controls : ParentUI
{
    [Export] ParentUI Keyboard { get; set; }
    [Export] ParentUI Controller { get; set; }

    public override void _Ready()
    {
        ApplyInputMode();

        InputManager.Instance.InputModeChanged += ApplyInputMode;
    }

    public override void _ExitTree()
    {
        InputManager.Instance.InputModeChanged -= ApplyInputMode;
    }

    private void ApplyInputMode()
    {
        switch (InputManager.Instance.CurrentInputMode)
        {
            case InputDeviceType.Controller:
                Keyboard.DeactivateNow();
                Controller.ActivateNow();
                break;
            case InputDeviceType.Keyboard:
                Keyboard.ActivateNow();
                Controller.DeactivateNow();
                break;
        }
    }
}
