namespace Game;

using Game.Player;
using BitterCitrus.SRC.Entites;
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

    public int CalPlayerAttackDamage(PlayerAttackType type)
    {
        

        return 0;
    }
}