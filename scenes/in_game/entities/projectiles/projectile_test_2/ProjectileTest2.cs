using PlayTest1;

using Godot;
using System;
using BitterCitrus.SRC.Entites;

public partial class ProjectileTest2 : Projectile
{
    public override void Destroy()
    {
        QueueFree();
    }

    public override void HandleMelee()
    {
        Vector2 playerPosition = CombatManager.Instance.Player.GlobalPosition;
        Vector2 bounceNormal = (GlobalPosition - playerPosition).Normalized();
        Launch(bounceNormal, LaunchSpeed / 2);
    }

    public override void HandleSmash()
    {
        
    }

    public override void HandleParry()
    {
        Vector2 playerPosition = CombatManager.Instance.Player.GlobalPosition;
        Vector2 bounceNormal = (GlobalPosition - playerPosition).Normalized();
        Launch(bounceNormal, LaunchSpeed);
    }

    public override void HandleMagic()
    {
        
    }

    public override void HandleNonPlayerAttack()
    {
        
    }
}
