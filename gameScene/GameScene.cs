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
    private PackedScene memoryTile;

    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        gameManager = GetNode<GameManager>("/root/GameManager");
        imageManager = GetNode<ImageManager>("/root/ImageManager");
        gridContainer = GetNode<GridContainer>("MG/HB/GCContainer/GC");
        memoryTile = GD.Load<PackedScene>("res://gameScene/memory_tile.tscn");
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

    private List<ImageResource> GetRandomSprites(int count)
    {
        var imageData = imageManager.ImageData.ToList();
        var end = imageData.Count()-1;
        List<ImageResource> images = [];

        while (images.Count()<count)
        {
            var index = GD.RandRange(0, end);
            images.Add(imageData[index]);
            (imageData[end], imageData[index]) = (imageData[index], imageData[end]);
            end --;
        }
        var shuffledList = images.Select(image=>new ImageResource(image.Name,image.Image)).ToList();
        shuffledList = ShuffleList(shuffledList);
        shuffledList.ForEach(item =>
        {
            images.Add(item);
        });
        return images;
    }
    private void SetGameLevel(int levelNumber)
    {
        var levelConfig = gameManager.LevelConfig[levelNumber];
        var rows = levelConfig.Row;
        var columns = levelConfig.Column;
        gridContainer.Columns = columns;

        var uniqueSpriteCount = rows*columns/2;
        var sprites = GetRandomSprites(uniqueSpriteCount);

        sprites.ForEach(sprite =>
        {
            var tile = memoryTile.Instantiate<TextureButton>();
            tile.GetNode<TextureRect>("ImageContainer/Image").Texture = sprite.Image;
            tile.GetNode<TextureRect>("ImageContainer/Tile").Texture = imageManager.GetRandomFrame().Image;
            gridContainer.AddChild(tile);
        });
        


    }
    public void OnExitButtonPressed()
    {
        signalManager.EmitSignal(SignalManager.SignalName.ExitGame);
    }
}
