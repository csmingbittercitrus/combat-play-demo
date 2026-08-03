namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

// Title Screen에서 사용할 선택지 UI.
// 기본적으로 Focus 및 Mouse 둘 다에 반응하며, 최상위 노드의 BehaviorRecursive의 

// Title UI에서 사용할 선택지로, ui_accept 입력을 받으면 시그널을 방출함.
// 기본적으로 MouseMode = Stop, FocusMode = All : 포커스와 마우스 둘 다에 반응함.
// 각각 최상위 노드(TitleScreen)의 BehaviorRecursive를 통해 일괄적으로 제어함.

public partial class UISelection : Control
{
    [Signal]
    public delegate void UISelectionSelectedEventHandler();

    [Export] private AnimationPlayer animation { get; set; }

    public override void _Ready()
    {
        animation.Play("RESET");
    }

    private void _on_focus_entered()
    {
        animation.Play("get_focus");
    }

    private void _on_focus_exited()
    {
        animation.Play("release_focus");
    }

    private void _on_mouse_entered()
    {
        animation.Play("get_focus");
    }

    private void _on_mouse_exited()
    {
        animation.Play("release_focus");
    }

    private void _on_gui_input(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.UI_Accept))
        {
            EmitSignalUISelectionSelected();
        }
    }
}
