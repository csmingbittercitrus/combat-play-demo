namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System.Collections.Generic;

public partial class Fall : PlayerState
{
    [Export] private RayCast2D LedgeRay { get; set; }

    // 코요테 관련 로직들을 fsm에 이관해야 할 것 같기도 함
    List<StringName> groundedStates = new List<StringName>
    {
        PlayerStateNames.Idle,
        PlayerStateNames.Walk,
    };

    public override void Enter()
    {
        if (groundedStates.Contains(FSM.PrevStateName))
        {
            FSM.CreateKoyoteTime();
        }
    }

    public override void Exit()
    {
        FSM.ConsumeKoyoteTime();
    }

    public override void ApplyVelocity(double delta)
    {
        float targetVelocity = FSM.InputAxis_X * FSM.Stats.WalkSpeed;

        if (Mathf.Abs(FSM.PlayerVelocity.X) > FSM.Stats.WalkSpeed)
        {
            FSM.PlayerVelocity.X = Mathf.MoveToward(FSM.PlayerVelocity.X, targetVelocity, (float)delta * FSM.Stats.SprintJumpAccel_X);
        }
        else
        {
            FSM.PlayerVelocity.X = FSM.InputAxis_X * FSM.Stats.WalkSpeed;
        }


        FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Player.GetGravity().Y);

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        if (FSM.Player.IsOnFloor())
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
        if (@event.IsActionPressed(InputActionNames.Jump))
        {
            if (FSM.CanKoyoteJump)
            {
                EmitSignalStateSwitchRequested(PlayerStateNames.Jump);
            }
            else
            {
                EmitSignalStateSwitchRequested(PlayerStateNames.Glide);
            }
        }
        else if (@event.IsActionPressed(InputActionNames.Dash))
        {
            if (FSM.CanDashInAir)
            {
                EmitSignalStateSwitchRequested(PlayerStateNames.DashInAir);
            }
        }
    }
}
