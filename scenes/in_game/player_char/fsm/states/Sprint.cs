namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Sprint : PlayerState
{
    public override void Enter()
    {
        if (FSM.PrevStateName == PlayerStateNames.Dash)
        {
            FSM.CurrentActionDirection = FSM.PrevActionDirection;
        }
        else
        {
            FSM.CurrentActionDirection = FSM.LastInputAxis_X;
        }
    }

    public override void Exit()
    {
        FSM.PrevActionDirection = FSM.CurrentActionDirection;
        FSM.CurrentActionDirection = 0.0f;
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.CurrentActionDirection * FSM.Stats.SprintSpeed;
        FSM.PlayerVelocity.Y = 1.0f;
    }

    public override void CheckIfSwitchState(double delta)
    {
        if (!FSM.Player.IsOnFloor())
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
        else if (FSM.InputAxis_X == -FSM.CurrentActionDirection)
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Brake);
        }
        else if (!Input.IsActionPressed(InputActionNames.Dash))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Brake);
        }
    }

    public override void HandleInputEvent(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.Jump))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.SprintJump);
        }
        else if (@event.IsActionReleased(InputActionNames.Dash))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Brake);
        }
    }
}
