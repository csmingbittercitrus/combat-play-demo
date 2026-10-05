namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;

using Godot;
using System;
[Tool]
[GlobalClass]
public partial class BackJump : BaseLeaf
{
    [Export] public float JumpSpeedX { get; private set; } = 250f;
    [Export] public float JumpSpeedY { get; private set; } = -400f;
    [Export] public float MinAirTime { get; private set; } = 0.3f;

    private bool Started = false;
    private bool Ground = false;

    public override void Quit()
    {
        Started = false;
        Ground = false;
        duration = 0;
        GD.Print($"[BackJump] backjump End");
    }

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        
        if(! Started)
        {
            
            GD.Print($"[BackJump] backjump start");
            Started = true;
            Ground = false;



            enemy.Animation.Play("backjump");

            Vector2 acc = enemy.Velocity;
            acc.X += enemy.Direction * JumpSpeedX;
            acc.Y = JumpSpeedY;
            enemy.Velocity = acc; 
            return TickResultEnum.RUNNING;
        }

        if (duration > MinAirTime && enemy.IsOnFloor())
        {
            Ground = true;

        }

        if(Ground)
        {
            Vector2 acc = enemy.Velocity;
            acc.X = 0f;
            enemy.Velocity = acc;

            Quit();
            return TickResultEnum.SUCCESS;
        }


        duration += delta;
        
        return TickResultEnum.RUNNING;
    }
}
