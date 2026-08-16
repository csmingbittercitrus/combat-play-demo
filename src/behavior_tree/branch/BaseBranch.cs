namespace BitterCitrus.SRC.BT;

using BitterCitrus.SRC.Entites;
using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public abstract partial class BaseBranch : BaseBTNode
{
    protected int RunningChildIndex { get; set; } = 0;
    protected BaseBTNode ChildToTick { get; set; } = null;
    protected Array<BaseBTNode> children { get; set; }

    public override void _Ready()
    {
        children = new();

        foreach (Node node in GetChildren())
        {
            if (node is BaseBTNode btNode)
            {
                children.Add(btNode);
            }
            else
            {
                GD.PrintErr($"[{this.Name}] Branch 노드가 BaseBTNode 외 자식 노드를 가지고 있음 : {node.Name}");
            }
        }

        if (children.Count < 2)
        {
            GD.PrintErr($"[{this.Name}] Branch 노드의 BaseBTNode 자식이 2개 미만");
        }
    }

    public override abstract TickResultEnum Tick(Enemy enemy, Blackboard blackboard, float delta);

    public override void Quit()
    {
        children[RunningChildIndex].Quit();
        RunningChildIndex = 0;
    }

}
