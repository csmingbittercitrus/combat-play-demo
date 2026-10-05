namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using System;

[Tool]
[GlobalClass]
public partial class BehaviorTree : Node
{
    #region Settings
    [ExportCategory("Settings")]


    private bool _enabled = true;
    [Export] public bool Enabled
    {
        get => _enabled;
        set
        {
            if (value)
            {
                _enabled = value;
                UpdateProcessMode();
            }
        }
    }

    private ProcessThreadEnum _processThread = ProcessThreadEnum.PHYSICS;

    [Export] public ProcessThreadEnum ProcessThread
    {
        get => _processThread;

        set
        {
            _processThread = value;
            UpdateProcessMode();
        }
    }

    private void UpdateProcessMode()
    {
        if (!Engine.IsEditorHint())
        {
            SetProcess(_enabled && _processThread == ProcessThreadEnum.IDLE);
            SetPhysicsProcess(_enabled && _processThread == ProcessThreadEnum.PHYSICS);
        }
    }
    #endregion


    [ExportCategory("Nodes")]
    [Export] public CharacterBody2D Body { get; private set; }

    private Enemy _body { get; set; }
    [Export] public CombatManager blackboard { get; private set; }

    private BaseBTNode NodeToTick { get; set; }

    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            return;
        }

        UpdateProcessMode();
        
        if (blackboard == null)
        {
            GD.PrintErr($"[{this.Name}] blackboard 없음");
        }
        
        if (GetChildCount() != 1)
        {
            GD.PrintErr($"[{this.Name}] 자녀의 수가 하나가 아님.");
        }

        if (Body is Enemy enemy)
        {
            _body = enemy;
        }

        if (GetChild<Node>(0) is BaseBTNode btNode)
        {
            NodeToTick = btNode;
        }
        else
        {
            GD.PrintErr($"[{Name}] : ㅇㄴㄹㅇ");
        }
        blackboard = CombatManager.Instance;
    }

    public override void _Process(double delta)
    {
        Tick((float)delta);
    }


    public override void _PhysicsProcess(double delta)
    {
        Tick((float)delta);
    }

    public TickResultEnum Tick(float delta)
    {
        if (Engine.IsEditorHint()) return TickResultEnum.FAILURE;

        TickResultEnum result = TickResultEnum.FAILURE;

        //blackboard.Tick(_body, blackboard, delta);

        Node child = GetChild<Node>(0);

        result = NodeToTick.Tick(_body, blackboard, delta);

        return result;
    }

    public void Quit()
    {
        
    }
}

public enum ProcessThreadEnum
{
    IDLE = 0,
    PHYSICS = 1,
    MANUAL = 2
}

public enum TickResultEnum
{
    SUCCESS = 0,
    FAILURE = 1,
    RUNNING = 2,
}