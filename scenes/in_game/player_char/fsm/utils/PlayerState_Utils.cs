namespace Game.Player.FSM;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;

public static class PlayerState_Utils
{
    public static void SwitchStateToIdleOrWalk(this PlayerState state)
    {
        if (Input.IsActionPressed(InputActionNames.Dash))
        {
            state.EmitSignal(PlayerState.SignalName.StateSwitchRequested, PlayerStateNames.Sprint);
        }
        else if (state.FSM.InputAxis_X == 0.0f)
        {
            state.EmitSignal(PlayerState.SignalName.StateSwitchRequested, PlayerStateNames.Idle);
        }
        else
        {
            state.EmitSignal(PlayerState.SignalName.StateSwitchRequested, PlayerStateNames.Walk);
        }
    }
}
