namespace BitterCitrus.SRC.Entites;

using Game;
using Game.Player;
using Godot;
using System;

public partial class Spike : CharacterBody2D
{
	[Export] Area2D area;

    public override void _Ready()
    {
        area.BodyEntered += OnAreaBodyEntered;
    }

    private void OnAreaBodyEntered(Node2D body)
    {
        if (body is PlayerChar player)
        {
            player.HandleDamage(DamageType.Spike);
        }
    }
}