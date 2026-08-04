namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;
using Godot;
using System;

public partial class Spin : PlayerState
{
    [Export] Timer SpinDuration { get; set; }

    public override void Enter()
    {
        SpinDuration.Start();
    }

    public override void Exit()
    {
        SpinDuration.Stop();
    }

    public override void ApplyVelocity(double delta)
    {
        FSM.PlayerVelocity.X = FSM.InputAxis_X * FSM.Stats.WalkSpeed;
        FSM.PlayerVelocity.Y = Mathf.MoveToward(FSM.PlayerVelocity.Y, FSM.Stats.MaxFallSpeed, (float)delta * FSM.Player.GetGravity().Y);

        if (FSM.PlayerVelocity.Y > FSM.Stats.MaxFallSpeedDuringSpin)
        {
            FSM.PlayerVelocity.Y = FSM.Stats.MaxFallSpeedDuringSpin;
        }

        FSM.FacingDirection = FSM.LastInputAxis_X;
    }
    
    public override void CheckIfSwitchState(double delta)
    {
        
    }

    public override void HandleInputEvent(InputEvent @event)
    {
        
    }

    private void _on_spin_duration_timeout()
    {
        EmitSignalStateSwitchRequested(PlayerStateNames.Fall);
    }
}
