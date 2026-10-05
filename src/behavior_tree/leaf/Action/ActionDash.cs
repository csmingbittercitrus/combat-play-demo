using BitterCitrus.SRC.BT;
using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class ActionDash : BaseLeaf
{
    [Export] public float DashDistance { get; private set; } = 300f;
    [Export] public float DashSpeed { get; private set; } = 1200f;

    private bool Started = false;
    private float dashDir = 1.0f;
    private Vector2 StartPos;
    public override void Quit()
    {
        Started = false;
        duration = 0;

    }

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        if (!Started)
        {
            Started = true;
            duration = 0f;
            StartPos = enemy.GlobalPosition;

            dashDir = enemy.Direction;
            return TickResultEnum.RUNNING;
        }

        duration += delta;


        float moved = Mathf.Abs(enemy.GlobalPosition.X - StartPos.X);


        bool reached = moved >= DashDistance;

        if (reached)
        {
            enemy.Velocity = new Vector2(0f, enemy.Velocity.Y);
            Quit();
            return TickResultEnum.SUCCESS;
        }

        enemy.Velocity = new Vector2(-(dashDir * DashSpeed), enemy.Velocity.Y);
        return TickResultEnum.RUNNING;
    }
}
