using Godot;
using System;

public partial class SignalManager : Node
{
    [Signal] public delegate void ExitGameEventHandler();
    [Signal] public delegate void StartLevelEventHandler(int level);
    [Signal] public delegate void TileRevealedEventHandler(MemoryTile memoryTile);
    [Signal] public delegate void GameOverEventHandler();
    [Signal] public delegate void UpdateScoreEventHandler(int moves, int pairs);
}
