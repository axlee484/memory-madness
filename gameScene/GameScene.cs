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

    private List<T> ShuffleList<T>(List<T> list)
    {
        var end = list.Count()-1;

        while (end > 0)
        {
            var randomIndex = GD.RandRange(0, end);
            (list[end], list[randomIndex]) = (list[randomIndex],list[end]);
            end --;
        }
        return list;
    }

    private List<(ImageResource,ImageResource)> GetRandomImages(int count)
    {
        var imageData = imageManager.ImageData.ToList();
        List<(ImageResource,ImageResource)> spriteAndFrameList = [];

        var end = imageData.Count()-1;
        while (spriteAndFrameList.Count()<count)
        {
            var index = GD.RandRange(0,end);
            spriteAndFrameList.Add((imageData[index],imageManager.GetRandomFrame()));
            (imageData[index], imageData[end]) = (imageData[end], imageData[index]);
            end--;
        }
        var duplicateList = ShuffleList(spriteAndFrameList.ToList());
        duplicateList.ForEach(spriteAndFrameList.Add);
        return spriteAndFrameList;
    }

    
    private void SetGameLevel(int levelNumber)
    {
        var levelConfig = gameManager.LevelConfig[levelNumber];
        var rows = levelConfig.Row;
        var columns = levelConfig.Column;
        gridContainer.Columns = columns;

        var uniqueSpriteCount = rows*columns/2;
        var spriteAndFrames = GetRandomImages(uniqueSpriteCount);
        
        spriteAndFrames.ForEach(item=>{
            var memoryTile = memoryTileScene.Instantiate<MemoryTile>();
            memoryTile.GetNode<TextureRect>("ImageContainer/Image").Texture = item.Item1.Image;
            memoryTile.GetNode<TextureRect>("ImageContainer/Tile").Texture = item.Item2.Image;
            gridContainer.AddChild(memoryTile);
        });
        

    }
    public void OnExitButtonPressed()
    {
        signalManager.EmitSignal(SignalManager.SignalName.ExitGame);
    }
}
