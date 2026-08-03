namespace Game.Player;

using Godot;
using System;

public partial class PlayerChar : CharacterBody2D
{
    public override void _Ready()
    {
        CombatManager.Instance.SetPlayer(this);
    }

}
