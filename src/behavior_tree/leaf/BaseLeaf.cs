namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

[Tool]
[GlobalClass]
public abstract partial class BaseLeaf : BaseBTNode
{
    public override string[] _GetConfigurationWarnings()
    {
        string[] warnings = new string[0];
        return warnings;
    }


    public override abstract TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta);

    public override abstract void Quit();
}
