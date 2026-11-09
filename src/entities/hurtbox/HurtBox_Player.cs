namespace BitterCitrus.SRC.Entites;

using Godot;
using System;
using System.Collections.Generic;



// 플레이어의 공격 판정이 상속하는 추상 클래스.
// (액션이나 공격이 아닌, 공격 판정.)

// Hurtbox_Hostile.TryClash 호출
// : 범위 내 Hurtbox_Hostile을 상쇄 시도

// IAttackable.HandlePlayerAttack 호출
// : 범위 내 IAttackable을 공격 시도



public abstract partial class HurtBox_Player : Area2D
{
    [Signal]
    public delegate void AttackFinishedEventHandler();



    #region Vars
    protected bool IsCooled { get; set; } = true;
    [Export] public Timer CooldownTimer { get; private set; }

    protected HashSet<Node2D> excludedTargets;
    [Export] protected PlayerAttackType type { get; set; }
    [Export] protected AnimationPlayer animation { get; private set; }

    protected bool IsActive { get; set; } = false;
    #endregion



    #region Cooldown
    public override void _Ready()
    {
        CooldownTimer.Timeout += OnCooldownTimerTimeout;
    }

    public override void _ExitTree()
    {
        CooldownTimer.Timeout -= OnCooldownTimerTimeout;
    }

    public virtual bool CheckIfCanExecuteAction()
    {
        return IsCooled;
    }

    private void OnCooldownTimerTimeout()
    {
        IsCooled = true;
    }
    #endregion



    #region Func
    public virtual void ActivateHurtBox()
    {
        excludedTargets = new();
        animation.Play("start_attack");
        IsCooled = false;
        CooldownTimer.Start();
        IsActive = true;
    }

    public virtual void DeactivateHurtBox()
    {
        EmitSignalAttackFinished();
        animation.Play("RESET");
        IsActive = false;
    }

    public void ProcessHurtBox()
    {
        GD.Print($"Name: {Name}");
        foreach (Area2D area in GetOverlappingAreas())
        {
            if (excludedTargets.Contains(area)) continue;
            if (area is HurtBox_Hostile action)
            {
                if (action.TryClash(type))
                {
                    OnParry();
                }
                excludedTargets.Add(area);
            }
        }

        foreach (Node2D body in GetOverlappingBodies())
        {
            if (excludedTargets.Contains(body)) continue;
            if (body is IAttackable target)
            {
                // Pogo 관련 기능 작동만 하게 해뒀음. OnHit, OnParry 포함해서 if문 안 뜯어고쳐야 함
                OnHit(target);
                target.HandlePlayerAttack(type);
                excludedTargets.Add(body);
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsActive)
        {
            ProcessHurtBox();
        }
    }
    #endregion



    #region Abstract
    public abstract void OnHit(IAttackable body);
    public abstract void OnParry();
    #endregion
}
