namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SequenceBranch : BaseBranch
{
    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        for (int i = RunningChildIndex; i < children.Count; i++)
        {
            ChildToTick = children[i];
            RunningChildIndex = i;

            switch (ChildToTick.Tick(enemy, blackboard, delta))
            {
                case TickResultEnum.FAILURE:
                    RunningChildIndex = 0;
                    return TickResultEnum.FAILURE;
                case TickResultEnum.SUCCESS:
                    break;
                case TickResultEnum.RUNNING:
                    return TickResultEnum.RUNNING;
            }
        }

        RunningChildIndex = 0;
        return TickResultEnum.SUCCESS;
    }
}
