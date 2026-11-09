namespace Game.Player.FSM;

using Godot;
using System;

public partial class WallJump : PlayerState
{
    [Export]
    private float ForceXMoveDuration { get; set; }
    [Export]
    private float Duration { get; set; }

    private Timer DurationTimer { get; set; }
    private Timer ForceXMoveDurationTimer { get; set; }

    #region LifeCycle
    public override void _Ready()
    {
        base._Ready();

        ForceXMoveDurationTimer = this.AddTimerToState(ForceXMoveDuration);

        DurationTimer = this.AddTimerToState(Duration);
        DurationTimer.Timeout += OnWallJumpDurationTimerTimeout;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        DurationTimer.Timeout -= OnWallJumpDurationTimerTimeout;
    }
    #endregion



    #region PlayerState
    public override void Enter()
    {
        FSM.CurrentActionDirection = -FSM.PrevActionDirection;
        FSM.FacingDirection = FSM.CurrentActionDirection;

        ForceXMoveDurationTimer.Start();
        DurationTimer.Start();

        FSM.PlayerVelocity.X = FSM.Stats.WallJumpSpeed_X * FSM.CurrentActionDirection;
        FSM.PlayerVelocity.Y = FSM.Stats.WallJumpSpeed_Y;

        FSM.Player.Velocity = FSM.PlayerVelocity;
    }

    public override void Exit()
    {
        FSM.PrevActionDirection = FSM.CurrentActionDirection;
        FSM.CurrentActionDirection = 0.0f;

        ForceXMoveDurationTimer.Stop();
        DurationTimer.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        float targetAccel;
        
        if (FSM.InputAxis_X == -FSM.CurrentActionDirection && !ForceXMoveDurationTimer.IsStopped())
        {
            targetAccel = FSM.Stats.WallJumpAccel_X_Fast;
        }
        else
        {
            targetAccel = FSM.Stats.WallJumpAccel_X;
        }

        FSM.PlayerVelocity.X = Mathf.MoveToward(FSM.PlayerVelocity.X, 0.0f, (float)delta * targetAccel);
        FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Stats.WallJumpAccel_Y);

    }

    public override void CheckIfSwitchState(double delta)
    {
        if (FSM.Player.IsOnCeiling())
        {
            FSM.PlayerVelocity.Y = 0.0f;
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
    }

    public override void HandleInputEvent(InputEvent @event)
    {
        
    }

    private void OnWallJumpDurationTimerTimeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
    }
    #endregion
}
