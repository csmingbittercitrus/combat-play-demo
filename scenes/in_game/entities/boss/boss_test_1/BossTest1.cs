namespace BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class BossTest1 : Enemy
{
    public override void HandleMelee()
    {
        
    }
    public override void HandleSmash()
    {
        
    }
    public override void HandleParry()
    {
        
    }
    public override void HandleMagic()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

}
