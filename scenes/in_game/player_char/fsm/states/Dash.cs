namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public partial class Dash : PlayerState
{
    [Export] private Timer DashDuration { get; set; }
    [Export] private Timer DashCooldown { get; set; }

    private bool IsCooldown { get; set; } = true;

    public override bool CheckIfEnterable()
    {
        return IsCooldown;
    }

    public override void Enter()
    {
        FSM.CurrentActionDirection = FSM.LastInputAxis_X;

        DashDuration.Start();

        IsCooldown = false;
        DashCooldown.Start();
    }

    public override void Exit()
    {
        FSM.PrevActionDirection = FSM.CurrentActionDirection;
        FSM.CurrentActionDirection = 0.0f;

        DashDuration.Stop();
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

    private void _on_dash_duration_timeout()
    {
        this.SwitchStateToIdleOrWalk();
    }

    private void _on_dash_cooldown_timeout()
    {
        IsCooldown = true;
    }
}
