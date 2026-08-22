namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public abstract partial class BaseBTNode : Node
{
    public abstract TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta);

    public abstract void Quit();
}

