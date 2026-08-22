namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class AwaitAnimationLeaf : BaseLeaf
{
    [Export] AnimationPlayer Animation { get; set; }
    [Export] StringName AnimationName { get; set; } = "";
    private bool IsAnimationFinished = false;

    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            return;
        }

        Animation.AnimationFinished += _on_animation_finished;
    }

    public override TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta)
    {
        if (IsAnimationFinished)
        {
            IsAnimationFinished = false;
            return TickResultEnum.SUCCESS;
        }
        else
        {
            if (Animation.IsPlaying() && Animation.CurrentAnimation == AnimationName)
            {
                return TickResultEnum.RUNNING;
            }
            else
            {
                Animation.Play(AnimationName);
                return TickResultEnum.RUNNING;
            }
        }
    }

    private void _on_animation_finished(StringName animationName)
    {
        GD.Print(animationName);
        if (animationName == AnimationName)
        {
            IsAnimationFinished = true;
        }
    }

    public override void Quit()
    {
        Animation.Play("RESET");
        IsAnimationFinished = false;
    }
}
