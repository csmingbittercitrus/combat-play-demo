namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public abstract partial class PlayerState : Node
{
    [Signal]
    public delegate void StateSwitchRequestedEventHandler(StringName targetStateName);

    public Player_FSM FSM { get; private set; }

    [Export] public bool CanExecuteAction { get; private set; } = true;


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

    public abstract void HandleInputEvent(InputEvent @event);
}
