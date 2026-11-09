namespace BitterCitrus.SRC.Entites;

using Godot;
using System;

public abstract partial class Enemy : CharacterBody2D, IAttackable
{
    [Export] public int LevelCoefficient { get; private set; }
    
    [Export] public AnimationPlayer Animation { get; private set; }

    [Export] public int BaseMaxHP { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }



    public void HandlePlayerAttack(PlayerAttackType type)
    {

    }

    public void HandleDamage(DamageType type)
    {
        
    }
}
