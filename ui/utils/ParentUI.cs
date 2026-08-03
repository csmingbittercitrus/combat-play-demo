namespace CombatPlayDemo.UI;

using Godot;
using System;
using System.Threading.Tasks;

// 하위 노드들의 활성화 여부를 관리하는 부모 UI 노드.

public partial class ParentUI : Control
{
    [Export] public Control UIToGiveInitialFocus { get; private set; }
    [Export] public float FadeInDuration { get; private set; }
    [Export] public float FadeOutDuration { get; private set; }

    public bool IsActive { get; set; }

    public override void _Ready()
    {
        Init();
    }

    protected virtual void Init()
    {
        IsActive = false;

        this.FocusMode = FocusModeEnum.None;
        this.MouseFilter = MouseFilterEnum.Ignore;

        this.ForceHideUI();

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Disabled;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Disabled;
    }

    public virtual async Task Activate()
    {
        await this.FadeInUI();

        IsActive = true;

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Inherited;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Inherited;

        UIToGiveInitialFocus?.GrabFocus();
    }

    public virtual async Task Deactivate()
    {
        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Disabled;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Disabled;

        IsActive = false;

        await this.FadeOutUI();
    }

    public virtual void ActivateNow()
    {
        this.Modulate = new Color(1, 1, 1, 1);
        Show();

        IsActive = true;

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Inherited;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Inherited;

        UIToGiveInitialFocus?.GrabFocus();
    }

    public virtual void DeactivateNow()
    {
        Hide();
        this. Modulate = new Color(1, 1, 1, 0);

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Disabled;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Disabled;

        IsActive = false;
    }


    public virtual void TryToGiveFocus()
    {
        if (!IsActive) return;

        UIToGiveInitialFocus?.GrabFocus();
    }
}
