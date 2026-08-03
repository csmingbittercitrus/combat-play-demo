namespace PlayTest1;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class Cannon1 : Node2D
{
    [Export] private string ProjectilePath { get; set; }

    [Export] Timer LaunchTimer { get; set; }

    private PackedScene Prefab;

    [Export] private Vector2 LaunchDirection { get; set; }

    public override void _Ready()
    {
        Prefab = ResourceLoader.Load<PackedScene>(ProjectilePath);

        LaunchTimer.Start();
    }

    public void Launch()
    {
        Projectile projectile = Prefab.Instantiate<Projectile>();

        GetTree().CurrentScene.AddChild(projectile);
        projectile.GlobalPosition = this.GlobalPosition;
        projectile.Launch(LaunchDirection);
    }

    private void _on_launch_timer_timeout()
    {
        Launch();
    }
}
