namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;

public partial class Jump : PlayerState
{
    [Export] private float Duration { get; set; }
    [Export] RayCast2D LedgeRay { get; set; }

    Timer DurationTimer { get; set; }



    #region LifeCycle
    public override void _Ready()
    {
        base._Ready();
        
        DurationTimer = this.AddTimerToState(Duration);
        DurationTimer.Timeout += OnDurationTimerTimeout;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        DurationTimer.Timeout -= OnDurationTimerTimeout;
    }
    #endregion



    #region PlayerState
    public override void Enter()
    {
        FSM.ConsumeJumpBuffer();

        FSM.PlayerVelocity.Y = FSM.Stats.JumpSpeed;
        FSM.Player.Velocity = FSM.PlayerVelocity;

        DurationTimer.Start();
    }

    public override void Exit()
    {
        DurationTimer.Stop();
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
        if (@event.IsActionReleased(InputActionNames.Jump))
        {
            EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
        }
    }

    private void OnDurationTimerTimeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
    }
    #endregion
}
