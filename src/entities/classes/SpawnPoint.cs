namespace BitterCitrus.SRC.Entites;

using Game.Player;

using Godot;
using System;

public partial class SpawnPoint : Node2D
{
	[Export] public Area2D Area { get; private set; }

	public override void _Ready()
	{
		Area.BodyEntered += SetSpawnPoint;
	}

	public Vector2 GetSpawnPointGlobalPosition()
	{
		return GetGlobalPosition();
	}

	private void SetSpawnPoint(Node2D body)
	{
		if (body is PlayerChar player)
		{
			player.SetSpawnPoint(this);
		}
	}
}
