namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class AwaitMoveLeaf : BaseBTNode
{
    [Export] private Vector2 InitialVelocity { get; set; }
    private Vector2 _initialVelocity { get; set; }
    [Export] private Vector2 TargetVelocity;
    private Vector2 _targetVelocity { get; set; }
    [Export] private Vector2 Accel { get; set; }

    [Export] private float Duration { get; set; } = 0.1f;

    private float elapsed { get; set; } = 0.0f;

    private Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {

    }


    public override TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta)
    {
        Vector2 velocity;

        if (elapsed == 0.0f)
        {
            elapsed += delta;

            if (CombatManager.Instance.Player != null)
            {
                float playerDirection = CombatManager.Instance.Player.GlobalPosition.X - enemy.GlobalPosition.X;

                if (playerDirection > 0)
                {
                    _initialVelocity = new Vector2(-InitialVelocity.X, InitialVelocity.Y);
                    _targetVelocity = new Vector2(-TargetVelocity.X, TargetVelocity.Y);
                }
                else
                {
                    _initialVelocity = new Vector2(InitialVelocity.X, InitialVelocity.Y);
                    _targetVelocity = new Vector2(TargetVelocity.X, TargetVelocity.Y);
                }
            }

            velocity = _initialVelocity;
            velocity.X = Mathf.MoveToward(velocity.X, _targetVelocity.X, delta * Accel.X);
            velocity.Y = Mathf.MoveToward(velocity.Y, _targetVelocity.Y, delta * Accel.Y);
            enemy.Velocity = velocity;

            return TickResultEnum.RUNNING;
        }

        else if (elapsed < Duration)
        {
            elapsed += delta;

            velocity = enemy.Velocity;
            velocity.X = Mathf.MoveToward(velocity.X, TargetVelocity.X, delta * Accel.X);
            velocity.Y = Mathf.MoveToward(velocity.Y, TargetVelocity.Y, delta * Accel.Y);
            enemy.Velocity = velocity;

            return TickResultEnum.RUNNING;
        }

        else
        {
            enemy.Velocity = Vector2.Zero;
            elapsed = 0.0f;
            return TickResultEnum.SUCCESS;
        }
    }

    public override void Quit()
    {
        elapsed = 0.0f;
    }
}
