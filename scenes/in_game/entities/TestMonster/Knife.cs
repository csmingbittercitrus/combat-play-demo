using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class Knife : Projectile
{
    public override void Destroy()
    {
        QueueFree();
    }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }



    public override void HandleMagic()
    {
        Destroy();
    }

    public override void HandleMelee()
    {
        Destroy();
    }

    public override void HandleParry()
    {
        Destroy();
    }

    public override void HandleSmash()
    {
        Destroy();
    }
}
