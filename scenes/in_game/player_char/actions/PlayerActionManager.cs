namespace Game.Player.Actions;

using BitterCitrus.SRC.Core.BInput;
using BitterCitrus.SRC.Entites;
using BitterCitrus.SRC.Handheld;
using Game.Player.FSM;
using Godot;
using System;


public partial class PlayerActionManager : Node2D
{
    [Export] PlayerChar Player { get; set; }

    [Export] Player_FSM FSM { get; set; }
    public bool IsHoldingItem { get; private set; } = false;
    public HandheldItem HoldingItem { get; private set; }

    // 공용 후딜레이 관리
    public bool CanExecuteAction { get; private set; } = true;
    [Export] Timer AfterCastDelayTimer { get; set; }


    [Export] HurtBox_Player Melee { get; set; }
    [Export] HurtBox_Player Parry { get; set; }
    [Export] HurtBox_Player ParryInAir { get; set; }

    [Export] Area2D PickUpArea { get; set; }

    [Export] Node2D CurrentHandPosition { get; set; }
    [Export] Node2D Hand { get; set; }

    private HurtBox_Player CurrentAction;

    public override void _Ready()
    {
        Melee.AttackFinished += FinishAttack;
        Parry.AttackFinished += FinishAttack;
        ParryInAir.AttackFinished += FinishAttack;

        CurrentAction = null;
    }

    private void FinishAttack()
    {
        CurrentAction = null;
    }


    public void ExecuteAction(StringName actionName)
    {
        if (!CanExecuteAction)
        {
            return;
        }
        
        if (CurrentAction != null)
        {
            return;
        }

        if (actionName == PlayerActionNames.Melee)
        {
            ExecuteAction_Right_Hand();
        }
        else if (actionName == PlayerActionNames.Parry)
        {
            ExecuteAction_Left_Hand();
        }
        else if (actionName == PlayerActionNames.Smash)
        {
            
        }
        else if (actionName == PlayerActionNames.Throw)
        {
            ExecuteAction_Throw();
        }
    }

    public void TryPickUpItems()
    {
        if (!IsHoldingItem)
        {
            foreach (Node2D body in PickUpArea.GetOverlappingBodies())
            {
                if (body is HandheldItem item)
                {
                    PickUpItem(item);
                    break;
                }
            }
        }
        else
        {
            ReleaseItem();
        }

    }


    public void ExecuteAction_Right_Hand()
    {
        if (IsHoldingItem)
        {
            HoldingItem.ActivateAction_Right_Hand(FSM.FacingDirection, FSM.InputAxis_Y);
        }
        else
        {
            CurrentAction = Melee;
            Melee.Scale = new Vector2(-FSM.FacingDirection, 1.0f);
            CurrentAction.ActivateHurtBox();
            StartAfterCastDelay(0.4f);
        }
    }

    public void ExecuteAction_Left_Hand()
    {
        if (IsHoldingItem)
        {
            
        }
        else
        {
            if (Player.IsOnFloor())
            {
                CurrentAction = Parry;
                Parry.Scale = new Vector2(-FSM.FacingDirection, 1.0f);
                CurrentAction.ActivateHurtBox();
                StartAfterCastDelay(0.4f);
            }
            else
            {
                CurrentAction = ParryInAir;
                ParryInAir.Scale = new Vector2(-FSM.FacingDirection, 1.0f);
                CurrentAction.ActivateHurtBox();
                StartAfterCastDelay(0.4f);
            }
        }
    }

    public void ExecuteAction_Throw()
    {
        GD.Print("!");
        if (IsHoldingItem)
        {
            GD.Print("!");
            HoldingItem.ActivateAction_Throw(FSM.FacingDirection, FSM.InputAxis_Y);
            ReleaseItem();
        }
        else
        {
            
        }
    }

    public void ExecuteAction_Smash()
    {
        if (IsHoldingItem)
        {

        }
        else
        {
            
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CurrentAction != null)
        {
            CurrentAction.ProcessHurtBox();
        }

        Hand.Scale = new Vector2(-FSM.FacingDirection, 1.0f);
    }


    public void StartAfterCastDelay(float duration)
    {
        CanExecuteAction = false;
        AfterCastDelayTimer.Start(duration);
    }

    private void _on_after_cast_delay_timeout()
    {
        CanExecuteAction = true;
    }

    public void PickUpItem(HandheldItem item)
    {
        IsHoldingItem = true;
        HoldingItem = item;
        HoldingItem.Reparent(CurrentHandPosition, true);
        HoldingItem.PickUp();
        HoldingItem.GlobalPosition = CurrentHandPosition.GlobalPosition;
    }

    public void ReleaseItem()
    {
        IsHoldingItem = false;
        HoldingItem?.Release();
        HoldingItem.Reparent(GetTree().CurrentScene, true);
        HoldingItem = null;
    }
}