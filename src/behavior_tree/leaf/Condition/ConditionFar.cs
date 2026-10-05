namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;

using Godot;
using System;
[Tool]
[GlobalClass]
public partial class ConditionFar : BaseLeaf
{
    [Export] public float Distance { get; private set; } = 150;
    public override void Quit()
    {

    }

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        float dist = enemy.GlobalPosition.DistanceTo(blackboard.Player.GlobalPosition);
        if(dist > Distance )
        {
            GD.Print("Far");
            return TickResultEnum.SUCCESS;
        }
        else
        {
            GD.Print("Near");
            return TickResultEnum.FAILURE;
        }
    }
}
