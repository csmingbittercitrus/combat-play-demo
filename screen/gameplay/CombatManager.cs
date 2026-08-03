using Game.Player;
using Game.Player.Actions;
using Godot;
using System;

public partial class CombatManager : Node
{
    public PlayerChar Player { get; private set; }

    public static CombatManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }


    public void SetPlayer(PlayerChar player)
    {
        Player = player;
    }
}
