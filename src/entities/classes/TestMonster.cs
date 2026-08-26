using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class TestMonster : Enemy
{

    public override void HandleMelee()
    {
        GD.Print($"HandleMelee");
    }
    public override void HandleSmash()
    {
        GD.Print($"HandleSmash");
    }
    public override void HandleParry()
    {
        GD.Print($"HandleParry");
    }
    public override void HandleMagic()
    {
        GD.Print($"HandleMagic");
    }
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }
    public override void HandleNonPlayerAttack() { }
}
