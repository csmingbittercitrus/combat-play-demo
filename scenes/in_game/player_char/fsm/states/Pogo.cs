namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public partial class Pogo : PlayerState
{
    [Export]
    private float Duration { get; set; }

    private Timer PogoDuration { get; set; }
    [Export] RayCast2D LedgeRay { get; set; }

    public override void _Ready()
    {
        base._Ready();

        PogoDuration = this.AddTimerToState(Duration);
        PogoDuration.Timeout += _on_pogo_duration_timeout;
    }

    public override void Enter()
    {
        FSM.ConsumeJumpBuffer();

        FSM.PlayerVelocity.Y = FSM.Stats.JumpSpeed;
        FSM.Player.Velocity = FSM.PlayerVelocity;

        PogoDuration.Start();
    }

    public override void Exit()
    {
        PogoDuration.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.InputAxis_X * FSM.Stats.WalkSpeed;
        FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Stats.JumpAccel_Y);

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        if (LedgeRay.IsColliding() && LedgeRay.GetCollisionNormal().IsEqualApprox(Vector2.Up))
        {
            if (FSM.FacingDirection == FSM.InputAxis_X)
            {
                EmitSignalStateSwitchRequested(PlayerStateNames.LedgeGrab);
            }
        }
        else if (FSM.Player.IsOnCeiling())
        {
            FSM.PlayerVelocity.Y = 0.0f;
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
    }

    public override void HandleInputEvent(InputEvent @event)
    {

    }

    private void _on_pogo_duration_timeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
    }
}

