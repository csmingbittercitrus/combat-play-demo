namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Game.Player;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Blackboard : Node
{
    PlayerChar Player => CombatManager.Instance.Player;

    public TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta)
    {
        return TickResultEnum.SUCCESS;
    }
}
