using Godot;
using System;
using System.Collections.Generic;

public record LevelConfigType(int Row, int Column);
public partial class GameManager : Node
{
    public IReadOnlyList<LevelConfigType> LevelConfig => _LevelConfig;
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

}
