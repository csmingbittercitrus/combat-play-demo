using PlayTest1;

using Game;
using Godot;
using System;
using BitterCitrus.SRC.Entites;

public partial class ProjectileTest2 : Projectile
{
    public override void Destroy()
    {
        QueueFree();
    }
}
