using Godot;
using System;
using System.Linq;

public partial class MainScene : Control
{
    private GameManager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        var levelConfig = gameManager.LevelConfig;
        var levelContainer = GetNode<HBoxContainer>("MC/VB/HBLevels");
        var levelButtonScene = GD.Load<PackedScene>("res://mainScene/level_button.tscn");


        for(var i = 0; i < levelConfig.Count; i++)
        {
            var levelButton = levelButtonScene.Instantiate<LevelButton>();
            levelButton.LevelNumber = i;
            levelContainer.AddChild(levelButton);
        }
        

    }
}
