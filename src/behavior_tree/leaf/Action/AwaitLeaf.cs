namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;

using Godot;
using System;

[Tool]
[GlobalClass]
public partial class AwaitLeaf : BaseLeaf
{
    [Export] private float Duration { get; set; } = 0.1f;

    private float elapsed { get; set; } = 0.0f;
    private bool isRunning { get; set; } = false;

    public override TickResultEnum Tick(Enemy enemy, CombatManager blackboard, float delta)
    {
        if (isRunning)
        {
            if (elapsed < Duration)
            {
                elapsed += delta;
                return TickResultEnum.RUNNING;
            }
            else
            {
                GD.Print($"[{Name}] Await 끝남");
                isRunning = false;
                elapsed = 0.0f;
                return TickResultEnum.SUCCESS;
            }
        }
        else if (elapsed == 0.0f)
        {
            enemy.Animation.Play("idle");
            GD.Print($"[{Name}] Await 시작");
            isRunning = true;
            elapsed += delta;
            return TickResultEnum.RUNNING;
        }

        return TickResultEnum.FAILURE;
    }

    public override void Quit()
    {
        elapsed = 0.0f;
        isRunning = false;
    }
}
