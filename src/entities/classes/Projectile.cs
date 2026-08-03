namespace BitterCitrus.SRC.Entites;

using Godot;
using System;

// 투사체.


public abstract partial class Projectile : Enemy, IAttackable
{
    #region Vars
    [Export] public float LaunchSpeed { get; private set; }



    [Export] public float Accel_X { get; private set; }
    [Export] public float GravityCoefficient { get; private set; } = 0.1f;



    [Export] public BounceTypeEnum BounceType { get; private set; }
    [Export] public float BounceCoefficient { get; private set; } = 0.5f;
    #endregion



    #region Funcs
    public void Launch(Vector2 direction)
    {
        Velocity = direction.Normalized() * LaunchSpeed;
    }

    public void Launch(Vector2 direction, float speed)
    {
        Velocity = direction.Normalized() * speed;
    }

    public override void _PhysicsProcess(double delta)
    {
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
    #endregion



    #region Abstract
    public abstract void Destroy();
    #endregion
}

public enum BounceTypeEnum
{
    Stop = 0,
    Bounce = 1,
    Fragile = 2,
}
