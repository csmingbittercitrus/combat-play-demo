namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Walk : PlayerState
{
    public override void Enter()
    {
        
    }
    public override void Exit()
    {
        
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.InputAxis_X * FSM.Stats.WalkSpeed;
        FSM.PlayerVelocity.Y = 1.0f;

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        if (FSM.InputAxis_X == 0.0f)
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Idle);
        }
        else if (!FSM.Player.IsOnFloor())
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
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
