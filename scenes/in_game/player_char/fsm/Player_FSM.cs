namespace Game.Player.FSM;

using Godot;
using System;
using System.Collections.Generic;
using BitterCitrus.SRC.Core.BInput;
using Game.Player.Actions;


public partial class Player_FSM : Node
{
    [Export] public PlayerChar Player { get; private set; }
    [Export] public PlayerActionManager ActionManager { get; private set; }

    private Dictionary<StringName, PlayerState> StateDict { get; set; }
    public PlayerState CurrentState { get; private set; }
    public StringName PrevStateName { get; private set; }
    public Vector2 PlayerVelocity = Vector2.Zero;

    public float InputAxis_X;
    public float InputAxis_Y;
    public float LastInputAxis_X;

    public float CurrentActionDirection;
    public float PrevActionDirection;
    public float FacingDirection;

    [Export] public FSMStats Stats { get; private set; }


    [Export] private Timer JumpBufferTimer { get; set; }
    [Export] private Timer KoyoteTimer { get; set; }

    public bool JumpBuffer;
    public bool CanKoyoteJump;

    [Export] public Label DebugLabel_PlayerState { get; private set; }
    [Export] public Label DebugLabel_InputAxis { get; private set; }

    public override void _Ready()
    {
        Init();
    }

    private void Init()
    {
        StateDict = new();

        foreach (Node child in this.GetChildren())
        {
            if (child is PlayerState state)
            {
                StateDict[child.Name] = state;
                state.StateSwitchRequested += SwitchState;
                state.ActionRequested += RequestAction;
            }
        }

        CurrentState = StateDict[PlayerStateNames.Idle];
        DebugLabel_PlayerState.Text = CurrentState.Name;

        PlayerVelocity = Vector2.Zero;

        InputAxis_X = 0.0f;
        InputAxis_Y = 0.0f;
        LastInputAxis_X = 1.0f;

        CurrentActionDirection = 0.0f;
        PrevActionDirection = 0.0f;
        FacingDirection = 1.0f;

        JumpBuffer = false;
        CanKoyoteJump = false;
    }

    #region States
    public override void _PhysicsProcess(double delta)
    {
        PlayerVelocity = Player.Velocity;
        CurrentState.ApplyVelocity(delta);
        Player.Velocity = PlayerVelocity;
        Player.MoveAndSlide();

        CurrentState.CheckIfSwitchState(delta);

        InputAxis_X = ModifyAxis(Input.GetAxis(InputActionNames.Left, InputActionNames.Right));
        if (InputAxis_X != 0.0f) LastInputAxis_X = InputAxis_X;
        InputAxis_Y = ModifyAxis(Input.GetAxis(InputActionNames.Up, InputActionNames.Down));

        DebugLabel_InputAxis.Text = InputAxis_X + ", " + InputAxis_Y;
    }

    private float ModifyAxis(float axisValue)
    {
        if (axisValue > 0.0f)
        {
            return 1.0f;
        }
        else if (axisValue < 0.0f)
        {
            return -1.0f;
        }
        else return 0.0f;
    }

    public void SwitchState(StringName targetStateName)
    {
        if (targetStateName == null) return;
        if (targetStateName == CurrentState.Name) return;
        
        if (StateDict.TryGetValue(targetStateName, out PlayerState targetState))
        {
            if (targetState.CheckIfEnterable())
            {
                PrevStateName = CurrentState.Name;
                CurrentState.Exit();
                CurrentState = targetState;
                CurrentState.Enter();
                DebugLabel_PlayerState.Text = CurrentState.Name;
            }
        }
        else
        {
            GD.PrintErr($"[Player_FSM] 유효하지 않은 stateName : {targetStateName}");
            return;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        CurrentState.HandleInputEvent(@event);
    }

    public void RequestAction(StringName actionName)
    {
        ActionManager.ExecuteAction(actionName);
    }
    #endregion



    #region BufferAndKoyote
    public void CreateJumpBuffer()
    {
        JumpBufferTimer.Start();
        JumpBuffer = true;
    }

    public void ConsumeJumpBuffer()
    {
        JumpBufferTimer.Stop();
        JumpBuffer = false;
    }

    private void _on_jump_buffer_timer_timeout()
    {
        JumpBuffer = false;
    }

    public void CreateKoyoteTime()
    {
        KoyoteTimer.Start();
        CanKoyoteJump = true;
    }

    public void ConsumeKoyoteTime()
    {
        KoyoteTimer.Stop();
        CanKoyoteJump = false;
    }

    private void _on_koyote_timer_timeout()
    {
        CanKoyoteJump = false;
    }
    #endregion
}
