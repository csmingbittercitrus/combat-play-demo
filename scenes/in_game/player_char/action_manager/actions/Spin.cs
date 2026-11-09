namespace Game.Player.Actions;

using Game.Player.FSM;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class Spin : HurtBox_Player
{
    [Export] private Player_FSM fsm { get; set; }

    public override void OnHit(IAttackable body)
    {
        if (body is PogoObject pogoObject)
        {
            if (pogoObject.GlobalPosition.Y > fsm.Player.GlobalPosition.Y)
            {
                fsm.SwitchState(PlayerStateNames.Pogo);
            }
        }
    }
    
    public override void OnParry()
    {
        
    }

    private void _on_body_entered(Node2D body)
    {

    }
}
