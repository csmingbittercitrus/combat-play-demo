namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;

using Godot;
using System;
using System.Threading;

[Tool]
[GlobalClass]
public partial class Dash : BaseLeaf
{
    [Export] public float DashDistance { get; private set; } = 300f;
    [Export] public float DashSpeed { get; private set; } = 1200f;

    private bool Started = false;
    private Vector2 _startPos;
    public override void Quit()
    {
        Started= false;
        duration= 0;

    }

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        if (!Started)
        {
            Started = true;
            duration = 0f;
            _startPos = enemy.GlobalPosition;

            enemy.Velocity = new Vector2(enemy.Direction * DashSpeed, 0f);
            return TickResultEnum.RUNNING;
        }

        duration += delta;

        
        float moved = Mathf.Abs(enemy.GlobalPosition.X - _startPos.X);

        
        bool reached = moved >= DashDistance;
        
        if (reached)
        {
            enemy.Velocity = new Vector2(0f, enemy.Velocity.Y);
            Quit();
            return TickResultEnum.SUCCESS;
        }

        enemy.Velocity = new Vector2(enemy.Direction * DashSpeed, enemy.Velocity.Y);
        return TickResultEnum.RUNNING;
    }
}
