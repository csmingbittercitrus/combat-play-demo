namespace Game.Player.FSM;

using Game.Player.Actions;

using Godot;
using System;

public partial class Bash : PlayerState
{
    [Export]
    private float Duration { get; set; }
    private Timer BashDurationTimer { get; set; }

    public override void _Ready()
    {
        base._Ready();

        BashDurationTimer = this.AddTimerToState(Duration); 
        BashDurationTimer.Timeout += OnBashDurationTimerTimeout;
    }

    public override void Enter()
    {
        BashDurationTimer.Start();
    }

    public override void Exit()
    {
        BashDurationTimer.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = Mathf.MoveToward(FSM.PlayerVelocity.X, 0.0f, (float)delta * FSM.Stats.AttackAccel_X);

        FSM.PlayerVelocity.Y = 1.0f;
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
        
    }

    private void OnBashDurationTimerTimeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Idle);
    }
}
