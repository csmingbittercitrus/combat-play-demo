namespace Game.Entitles;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

// 테스트용 투사체 1.
// 기본 공격이나 패링에 적중 시 파괴됨.
// 벽 또는 바닥에 충돌 시 파괴됨.

public partial class ProjectileTest1 : Projectile
{
    public override void Destroy()
    {
        QueueFree();
    }

    public override void HandleMelee()
    {
        Destroy();
    }

    public override void HandleSmash()
    {
        Destroy();
    }

    public override void HandleParry()
    {
        Destroy();
    }

    public override void HandleMagic()
    {
        Destroy();
    }
}
