namespace BitterCitrus.SRC.Handheld;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

public abstract partial class HandheldItem : Projectile
{
    public bool IsInHand { get; set; }

    public void PickUp()
    {
        IsInHand = true;
    }

    public void Release()
    {
        IsInHand = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsInHand)
        {
            Velocity = Vector2.Zero;
            return;
        }

        Vector2 velocity = Velocity;

        velocity.Y += GetGravity().Y * (float)delta * GravityCoefficient;
        velocity.X = Mathf.MoveToward(velocity.X, 0.0f, (float)delta * Accel_X);

        KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);

        if (collision != null)
        {
            switch(BounceType)
            {
                case BounceTypeEnum.Stop:
                    break;
                case BounceTypeEnum.Bounce:
                    velocity = velocity.Bounce(collision.GetNormal()) * BounceCoefficient;
                    break;
                case BounceTypeEnum.Fragile:
                    Destroy();
                    return;
            }
        }

        Velocity = velocity;
    }

    public abstract void ActivateAction_Right_Hand(float facingdirection, float InputAxis_Y);

    public abstract void ActivateAction_Left_Hand(float facingdirection, float InputAxis_Y);

    public abstract void ActivateAction_Throw(float facingdirection, float InputAxis_Y);

    public abstract void ActivateAction_Smash(float facingdirection, float InputAxis_Y);
    public override void HandleNonPlayerAttack() {}
}
