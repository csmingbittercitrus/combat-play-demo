using BitterCitrus.SRC.BT;
using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class TestMonster : Enemy
{
    [Export] public AnimatedSprite2D Sprite {  get; private set; }
    [Export] public PackedScene KnifeScene {  get; private set; }
    [Export] public Node2D Launcher {  get; private set; }


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
        
        Vector2 nodeDir = NodeToFlipX.Scale;
        nodeDir.X = -Direction;
        NodeToFlipX.Scale= nodeDir;



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
    public void LaunchKnfie()
    {
        Knife p = KnifeScene.Instantiate<Knife>();
        GetTree().CurrentScene.AddChild(p);
        p.GlobalPosition = Launcher.GlobalPosition;
        Vector2 dir = Launcher.GlobalPosition.DirectionTo(CombatManager.Instance.Player.GlobalPosition);

        p.Rotation = dir.Angle();
        p.Launch(dir, 1200); //temp magic num


    }
    public override void HandleNonPlayerAttack() { }
}
