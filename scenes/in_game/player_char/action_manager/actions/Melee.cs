namespace Game.Player.Actions;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class Melee : HurtBox_Player
{
    bool flag = false;

    public override void ActivateHurtBox()
    {
        excludedTargets = new();
        if (flag)
        {
            animation.Play("start_attack_1");
            flag = false;
        }
        else
        {
            animation.Play("start_attack_2");
            flag = true;
        }
        
        IsCooled = false;
        CooldownTimer.Start();
    }



    public override void OnHit()
    {
        
    }
    
    public override void OnParry()
    {
        
    }
}
