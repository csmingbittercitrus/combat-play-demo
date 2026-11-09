namespace Game.Player.FSM;

using Godot;
using System;

public partial class DashInAir : PlayerState
{
	[Export]
	private float Duration { get; set; }

	private Timer DashInAirDurationTimer { get; set; }

	public override void _Ready()
	{
		base._Ready();

		DashInAirDurationTimer = this.AddTimerToState(Duration);
		DashInAirDurationTimer.Timeout += _on_dash_in_air_duration_timeout;
	}

    public override bool CheckIfEnterable()
    {
        return FSM.CanDashInAir;
    }


    public override void Enter()
    {
        FSM.CanDashInAir = false;
		FSM.CurrentActionDirection = FSM.LastInputAxis_X;

		FSM.PlayerVelocity.X = FSM.Stats.DashInAirInitialVelocity_X * FSM.CurrentActionDirection;
		FSM.PlayerVelocity.Y = FSM.Stats.DashInAirInitialVelocity_Y;
		FSM.Player.Velocity = FSM.PlayerVelocity;

		DashInAirDurationTimer.Start();
    }

    public override void Exit()
    {
        DashInAirDurationTimer.Stop();
    }

	public override void ApplyVelocity(double delta)
	{
		FSM.PlayerVelocity.X = Mathf.MoveToward(FSM.PlayerVelocity.X, 0.0f, (float)delta * FSM.Stats.DashInAirAccel_X);
		FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Stats.DashInAirAccel_Y);
	}

	public override void CheckIfSwitchState(double delta)
	{

	}

	public override void HandleInputEvent(InputEvent @event)
	{
		
	}

	private void _on_dash_in_air_duration_timeout()
	{
		EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
	}
}
