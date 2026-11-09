namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Brake : PlayerState
{
    [Export]
    private float Duration { get; set; }
    private Timer DurationTimer { get; set; }


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
        FSM.CurrentActionDirection = FSM.PrevActionDirection;
        DurationTimer.Start();
    }

    public override void Exit()
    {
        FSM.PrevActionDirection = FSM.CurrentActionDirection;
        FSM.CurrentActionDirection = 0.0f;

        DurationTimer.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity = Vector2.Zero;

        FSM.FacingDirection = FSM.CurrentActionDirection;
    }

    public override void CheckIfSwitchState(double delta)
    {
        if (!FSM.Player.IsOnFloor())
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
    }

    private void OnDurationTimerTimeout()
    {
        this.SwitchStateToIdleOrWalk();
    }
    #endregion
}
