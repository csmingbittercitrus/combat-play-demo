namespace CombatPlayDemo.UI;

using Godot;
using System;
using BitterCitrus.SRC.Core.BInput;

public partial class SettingsTab : Control
{
    [Signal]
    public delegate void SettingsTabSelectedEventHandler(int index);

    public int Index { get; set; }

    [Export] ColorRect BG { get; set; }

    public override void _Ready()
    {
        BG.Hide();
    }

    public void ActivateNow()
    {
        BG.Show();
    }

    public void DeactivateNow()
    {
        BG.Hide();
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.UI_Accept))
        {
            EmitSignalSettingsTabSelected(Index);
        }
    }

}
