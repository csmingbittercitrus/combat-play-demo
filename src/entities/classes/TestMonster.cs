using BitterCitrus.SRC.BT;
using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class TestMonster : Enemy
{
    [Export] public AnimatedSprite2D Sprite {  get; private set; }
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
    public override void _Ready()
    {

    }
    public override void _PhysicsProcess(double delta)
    {
        Sprite.FlipH = Direction > 0f;
        

        Vector2 playerPos = CombatManager.Instance.Player.GlobalPosition;

        Direction = playerPos.X > GlobalPosition.X ? -1f : 1f;

        Vector2 vel = Velocity;
        if(!IsOnFloor())
        {
            vel.Y += GetGravity().Y  * (float)delta;
        }
        else if(vel.Y>0)
        {
            vel.Y = 0;
        }
        Velocity = vel;


        MoveAndSlide();
    }
    public override void HandleNonPlayerAttack() { }
}
