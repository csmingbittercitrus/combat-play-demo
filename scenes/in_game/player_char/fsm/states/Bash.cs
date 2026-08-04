namespace Game.Player.FSM;

using Game.Player.Actions;

using Godot;
using System;

public partial class Bash : PlayerState
{
    [Export] Timer BashDuration { get; set; }

    public override void Enter()
    {
        BashDuration.Start();
    }

    public override void Exit()
    {
        BashDuration.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity = Vector2.Down;
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

    private void _on_bash_duration_timeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Idle);
    }
}
