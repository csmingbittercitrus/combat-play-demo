namespace Game.Player;

using Game.Player.FSM;
using BitterCitrus.SRC.Entites;
using Godot;
using System;

public partial class PlayerChar : CharacterBody2D, IAttackable
{
    #region Vars
    [Signal]
    public delegate void PlayerDamagedEventHandler();

    [Export] protected Player_FSM fsm { get; private set; }

    [Export] private int InitialMaxHP { get; set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }

    private SpawnPoint currentSpawnPoint;
    [Export] Timer InvincibilityTimer { get; set; }
    #endregion



    public override void _Ready()
    {
        CombatManager.Instance.SetPlayer(this);
    }


    #region IAttackable
    public void HandlePlayerAttack(PlayerAttackType type)
    {
        
    }

    public void HandleDamage(DamageType type)
    {
        switch (type)
        {
            case DamageType.Normal:
                CurrentHP -= 1;
                break;
            case DamageType.Strong:
                CurrentHP -= 2;
                break;
            case DamageType.Spike:
                CurrentHP -= 1;
                MoveToCurrentSpawnPoint();
                break;
        }

        if (InvincibilityTimer.IsStopped())
        {
            fsm.SwitchState(PlayerStateNames.Damaged);
            InvincibilityTimer.Start();
            EmitSignalPlayerDamaged();
        }
    }
    #endregion
    

    #region SpawnPoints
    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

    public void MoveToCurrentSpawnPoint()
    {
        if (currentSpawnPoint != null)
        {
            GlobalPosition = currentSpawnPoint.GetSpawnPointGlobalPosition();
        }
    }
    #endregion
}
