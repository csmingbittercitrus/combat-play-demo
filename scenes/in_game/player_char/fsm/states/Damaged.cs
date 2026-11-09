namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public partial class Damaged : PlayerState
{
	[Export]
	private float StiffDuration { get; set; }
	[Export]
	private float MoveDuration { get; set; }

    private Timer StiffTimer { get; set; }
	private Timer MoveTimer { get; set; }

    public override void _Ready()
    {
        base._Ready();

        StiffTimer = this.AddTimerToState(StiffDuration);
        StiffTimer.Timeout += _on_stiff_timer_timeout;

        MoveTimer = this.AddTimerToState(MoveDuration);
        MoveTimer.Timeout += _on_move_timer_timeout;
    }

    public override void Enter()
    {
        StiffTimer.Start();
		FSM.PlayerVelocity = Vector2.Zero;
		FSM.Player.Velocity = FSM.PlayerVelocity;

		FSM.CurrentActionDirection = -FSM.FacingDirection;
    }

    public override void Exit()
    {
        StiffTimer.Stop();
		MoveTimer.Stop();
    }

	private void _on_stiff_timer_timeout()
	{
		MoveTimer.Start();

		FSM.PlayerVelocity.X = FSM.CurrentActionDirection * FSM.Stats.DamagedInitialVelocity_X;
		FSM.PlayerVelocity.Y = -FSM.Stats.DamagedInitialVelocity_Y;

		FSM.Player.Velocity = FSM.PlayerVelocity;
	}

	private void _on_move_timer_timeout()
	{
		EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
	}

    public override void ApplyVelocity(double delta)
    {
        if (MoveTimer.IsStopped())
		{
			return;
		}
		else
		{
			FSM.PlayerVelocity.X = Mathf.MoveToward(FSM.PlayerVelocity.X, 0.0f, (float)delta * FSM.Stats.DamagedAccel_X);
			FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, 0.0f, (float)delta * FSM.Stats.DamagedAccel_Y);
		}
    }
    
    public override void CheckIfSwitchState(double delta)
    {

    }

    public override void HandleInputEvent(InputEvent @event)
    {

    }
}
