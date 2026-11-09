namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Dash : PlayerState
{
    [Export] private float Duration { get; set; }
    [Export] private float Cooldown { get; set; }

    private Timer DurationTimer { get; set; }
    private Timer CooldownTimer { get; set; }


    #region LifeCycle
    public override void _Ready()
    {
        base._Ready();
        
        DurationTimer = this.AddTimerToState(Duration);
        DurationTimer.Timeout += OnDurationTimerTimeout;

        CooldownTimer = this.AddTimerToState(Cooldown);
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        DurationTimer.Timeout -= OnDurationTimerTimeout;
    }
    #endregion



    #region PlayerState
    public override bool CheckIfEnterable()
    {
        return CooldownTimer.IsStopped();
    }

    public override void Enter()
    {
        FSM.CurrentActionDirection = FSM.LastInputAxis_X;

        DurationTimer.Start();
        CooldownTimer.Start();
    }

    public override void Exit()
    {
        FSM.PrevActionDirection = FSM.CurrentActionDirection;
        FSM.CurrentActionDirection = 0.0f;

        DurationTimer.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.CurrentActionDirection * FSM.Stats.DashSpeed;
        FSM.PlayerVelocity.Y = 1.0f;

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
            EmitSignalStateSwitchRequested(PlayerStateNames.SprintJump);
        }
    }

    private void OnDurationTimerTimeout()
    {
        this.SwitchStateToIdleOrWalk();
    }
    #endregion
}
