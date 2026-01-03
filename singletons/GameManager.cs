using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public record LevelConfigType(int Row, int Column);
public partial class GameManager : Node
{
    public IReadOnlyList<LevelConfigType> LevelConfig => _LevelConfig;
    public string GROUP_MEMORY_TILE = "memory_tile";
    private readonly List<LevelConfigType> _LevelConfig = [];
    private void SetLevelConfig()
    {
        _LevelConfig.Add(new LevelConfigType(4, 3));
        _LevelConfig.Add(new LevelConfigType(4, 4));
        _LevelConfig.Add(new LevelConfigType(4, 5));
        _LevelConfig.Add(new LevelConfigType(4, 6));
        _LevelConfig.Add(new LevelConfigType(5, 6));
        _LevelConfig.Add(new LevelConfigType(6, 6));
    }
    public override void _Ready()
    {
        SetLevelConfig();
    }

    public void ClearNodesInGroup(string groupName)
    {
        GetTree().GetNodesInGroup(groupName).ToList().ForEach(node => node.QueueFree());
    }

}
