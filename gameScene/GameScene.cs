using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameScene : Control
{
    private SignalManager signalManager;
    private int gameLevel = 0;
    private GridContainer gridContainer;
    private GameManager gameManager;
    private ImageManager imageManager;
    private PackedScene memoryTileScene;

    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        gameManager = GetNode<GameManager>("/root/GameManager");
        imageManager = GetNode<ImageManager>("/root/ImageManager");
        gridContainer = GetNode<GridContainer>("MG/HB/GCContainer/GC");
        memoryTileScene = GD.Load<PackedScene>("res://gameScene/memory_tile.tscn");
        signalManager.StartLevel += SetGameLevel;
    }

    public void SetGameLevel(int level)
    {
        gameLevel = level;
        var spritesAndFrames = gameManager.SetGameLevel(gameLevel);
        for (var i = 0; i < spritesAndFrames.Count; i++)
        {
            var memoryTile = memoryTileScene.Instantiate<MemoryTile>();
            memoryTile.GetNode<TextureRect>("ImageContainer/Image").Texture = spritesAndFrames[i].Item1.Image;
            memoryTile.GetNode<TextureRect>("ImageContainer/Tile").Texture = spritesAndFrames[i].Item2.Image;
            memoryTile.TileName = spritesAndFrames[i].Item1.Name;
            gridContainer.Columns = gameManager.LevelConfig[gameLevel].Column;
            gridContainer.AddChild(memoryTile);

        }
    }


    public void OnExitButtonPressed()
    {
        signalManager.EmitSignal(SignalManager.SignalName.ExitGame);
    }
}
