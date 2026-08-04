namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public partial class Idle : PlayerState
{
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity = Vector2.Down;

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        if (FSM.InputAxis_X != 0.0f)
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Walk);
        }
        else if (!FSM.Player.IsOnFloor())
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
        else if (FSM.InputAxis_Y == 1.0f && FSM.InputAxis_X == 0.0f)
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Crouch);
        }
    }

    public override void HandleInputEvent(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActionNames.Jump))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Jump);
        }
        else if (@event.IsActionPressed(InputActionNames.Dash))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Dash);
        }
    }
}
