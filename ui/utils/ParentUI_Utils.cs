namespace CombatPlayDemo.UI;

using Godot;
using System;
using System.Threading.Tasks;

// ParentUI의 페이드 인/아웃 관련 확장 메서드를 담고 있는 클래스.

public static class ParentUI_Utils
{
    private static Color transparent { get; set; } = new Color(1, 1, 1, 0);
    private static Color opacity { get; set; } = new Color(1, 1, 1, 1);

    public static async Task FadeInUI(this ParentUI node)
    {
        if (node is null || !GodotObject.IsInstanceIdValid(node.GetInstanceId()))
        {
            GD.PrintErr("[UI_Utils] FadeIn 실패. 유효하지 않은 노드.");
            return;
        }

        if (node.Modulate.A == 1.0f && node.Visible) return;

        node.Modulate = transparent;
        node.Show();

        node.KillExistingTween();
        Tween tween = node.CreateTween();
        node.SetMeta("__fade_tween", tween);

        tween.TweenProperty(node, "modulate:a", 1, node.FadeInDuration);

        await node.ToSignal(tween, Tween.SignalName.Finished);
    }

    public static async Task FadeOutUI(this ParentUI node)
    {
        if (node is null || !GodotObject.IsInstanceIdValid(node.GetInstanceId()))
        {
            GD.PrintErr("[UI_Utils] FadeOut 실패. 유효하지 않은 노드.");
            return;
        }

        if (node.Modulate.A == 0 || !node.Visible) return;

        node.Modulate = opacity;
        node.Show();

        node.KillExistingTween();
        Tween tween = node.CreateTween();
        node.SetMeta("__fade_tween", tween);

        tween.TweenProperty(node, "modulate:a", 0, node.FadeOutDuration);

        await node.ToSignal(tween, Tween.SignalName.Finished);

        node.Hide();
    }

    public static void ForceShowUI(this ParentUI node)
    {
        if (node is null || !GodotObject.IsInstanceIdValid(node.GetInstanceId()))
        {
            GD.PrintErr("[UI_Utils] ForceShowUI 실패. 유효하지 않은 노드.");
            return;
        }

        node.KillExistingTween();

        node.Modulate = opacity;
        node.Show();
    }

    public static void ForceHideUI(this ParentUI node)
    {
        if (node is null || !GodotObject.IsInstanceIdValid(node.GetInstanceId()))
        {
            GD.PrintErr("[UI_Utils] ForceHideUI 실패. 유효하지 않은 노드.");
            return;
        }

        node.KillExistingTween();

        node.Modulate = transparent;
        node.Hide();
    }

    private static void KillExistingTween(this ParentUI node)
    {
        if (node.HasMeta("__fade_tween"))
        {
            Variant meta = node.GetMeta("__fade_tween");
            if (meta.AsGodotObject() is Tween tween && tween.IsValid())
            {
                tween.Kill();
            }
            node.RemoveMeta("__fade_tween");
        }
    }
}
