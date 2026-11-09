namespace BitterCitrus.SRC.BT;

using Godot;
using System;
using Game;
using BitterCitrus.SRC.Entites;

public partial class ConditionFindPlayer : BaseLeaf
{
    public override void Quit()
    {
        
    }

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {

        float dist = enemy.GlobalPosition.DistanceTo(blackboard.Player.GlobalPosition);
        
        if(enemy.AttackRange >= dist)
        {
            return TickResultEnum.SUCCESS;
        }
        return TickResultEnum.FAILURE;

    }
}
