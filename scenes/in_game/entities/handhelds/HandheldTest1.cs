using BitterCitrus.SRC.Entites;
using BitterCitrus.SRC.Handheld;
using Godot;
using System;

public partial class HandheldTest1 : HandheldItem
{
    public override void ActivateAction_Right_Hand(float facingdirection, float InputAxis_Y)
    {
        Vector2 direction = new Vector2(facingdirection, 0.0f);
        Launch(direction);
    }

    public override void ActivateAction_Left_Hand(float facingdirection, float InputAxis_Y)
    {
        
    }

    public override void ActivateAction_Throw(float facingdirection, float InputAxis_Y)
    {
        Vector2 direction = new Vector2(facingdirection, 0.0f);
        Launch(direction);
    }

    public override void ActivateAction_Smash(float facingdirection, float InputAxis_Y)
    {
        
    }



    public override void Destroy()
    {
        
    }

    public override void HandleMelee()
    {
        Vector2 playerPosition = CombatManager.Instance.Player.GlobalPosition;
        Vector2 bounceNormal = (GlobalPosition - playerPosition).Normalized();
        Launch(bounceNormal, LaunchSpeed / 2);
    }

    public override void HandleSmash()
    {
        
    }

    public override void HandleParry()
    {
        Vector2 playerPosition = CombatManager.Instance.Player.GlobalPosition;
        Vector2 bounceNormal = (GlobalPosition - playerPosition).Normalized();
        Launch(bounceNormal, LaunchSpeed / 2);
    }

    public override void HandleMagic()
    {
        
    }

    public override void HandleNonPlayerAttack()
    {
        
    }
}
