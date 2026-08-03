using Game.Player.FSM;

using Godot;
using System;

public partial class FSMStats : Resource
{
    [Export] public float WalkSpeed { get; private set; }
    [Export] public float JumpSpeed { get; private set; }
    [Export] public float JumpAccel { get; private set; }
    [Export] public float MaxFallSpeed { get; private set; }
    [Export] public float DashSpeed { get; private set; }
    [Export] public float SprintSpeed { get; private set; }
    [Export] public float DecelSpeed { get; private set; }
}
