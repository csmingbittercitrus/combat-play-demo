namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public abstract partial class PlayerState : Node
{
    [Signal]
    public delegate void StateSwitchRequestedEventHandler(StringName targetStateName);

    [Signal]

    public delegate void ActionRequestedEventHandler(StringName actionName);

    public Player_FSM FSM { get; private set; }


    public virtual bool CheckIfEnterable()
    {
        return true;
    }
    
    public abstract void Enter();
    public abstract void Exit();

    public override void _Ready()
    {
        FSM = GetParent<Player_FSM>();
    }

    public abstract void ApplyVelocity(double delta);
    
    public abstract void CheckIfSwitchState(double delta);

    public virtual void HandleInputEvent(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.Attack))
        {
            EmitSignalActionRequested(PlayerActionNames.Melee);
        }
        else if (@event.IsActionPressed(InputActionNames.Smash))
        {
            EmitSignalActionRequested(PlayerActionNames.Smash);
        }
        else if (@event.IsActionPressed(InputActionNames.Parry))
        {
            EmitSignalActionRequested(PlayerActionNames.Parry);
        }
        else if (@event.IsActionPressed(InputActionNames.Throw))
        {
            EmitSignalActionRequested(PlayerActionNames.Throw);
        }
    }

}
