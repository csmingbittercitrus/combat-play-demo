namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public partial class Spin : PlayerState
{
    [Export] Timer SpinDuration { get; set; }
    [Export] RayCast2D LedgeRay { get; set; }

    public override void Enter()
    {
        SpinDuration.Start();
    }

    public override void Exit()
    {
        SpinDuration.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.InputAxis_X * FSM.Stats.WalkSpeed;
        FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Player.GetGravity().Y);

        if (FSM.PlayerVelocity.Y > FSM.Stats.MaxFallSpeedDuringSpin)
        {
            FSM.PlayerVelocity.Y = FSM.Stats.MaxFallSpeedDuringSpin;
        }

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        if (FSM.Player.IsOnCeiling())
        {
            FSM.PlayerVelocity.Y = 0.0f;
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
		else if (FSM.Player.IsOnFloor())
        {
            this.SwitchStateToIdleOrWalk();
        }
        else if (LedgeRay.IsColliding() && LedgeRay.GetCollisionNormal().IsEqualApprox(Vector2.Up))
        {
            if (FSM.FacingDirection == FSM.InputAxis_X)
            {
                EmitSignalStateSwitchRequested(PlayerStateNames.LedgeGrab);
            }
        }
        else if (FSM.Player.IsOnWall() && FSM.Player.GetWallNormal().IsEqualApprox(new Vector2(-FSM.InputAxis_X, 0)) && FSM.PlayerVelocity.Y > 0)
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.WallSlipper);
        }
    }

    public override void HandleInputEvent(InputEvent @event)
    {
        
    }

    private void _on_spin_duration_timeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
    }
}