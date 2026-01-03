using Godot;
using System;

public partial class SignalManager : Node
{
    [Signal] public delegate void ExitGameEventHandler();
    [Signal] public delegate void StartLevelEventHandler(int level);
}
