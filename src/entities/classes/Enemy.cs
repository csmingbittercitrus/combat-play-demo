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
        switch(type)
        {
            case PlayerAttackType.Melee:
                HandleMelee();
                break;
            case PlayerAttackType.Smash:
                HandleSmash();
                break;
            case PlayerAttackType.Parry:
                HandleParry();
                break;
            case PlayerAttackType.Magic:
                HandleMagic();
                break;
        }
    }

    
    public abstract void HandleMelee();
    public abstract void HandleSmash();
    public abstract void HandleParry();
    public abstract void HandleMagic();

    public virtual void HandleNonPlayerAttack() {}

}
