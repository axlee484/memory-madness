using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public record LevelConfigType(int Row, int Column);
public partial class GameManager : Node
{
    public IReadOnlyList<LevelConfigType> LevelConfig => _LevelConfig;
    private List<string> levelSpritesList = []; 
    public string GROUP_MEMORY_TILE = "memory_tile";
    private readonly List<LevelConfigType> _LevelConfig = [];
    private SignalManager signalManager;
    private ImageManager imageManager;

    public int RevealedTilesCount {get; set;} = 0;
    private int gameLevel = 0;
    private int moves = 0;
    private int score = 0;
    private List<MemoryTile> revealedTiles = [];
    private void SetLevelConfig()
    {
        _LevelConfig.Add(new LevelConfigType(4, 3));
        _LevelConfig.Add(new LevelConfigType(4, 4));
        _LevelConfig.Add(new LevelConfigType(4, 5));
        _LevelConfig.Add(new LevelConfigType(4, 6));
        _LevelConfig.Add(new LevelConfigType(5, 6));
        _LevelConfig.Add(new LevelConfigType(6, 6));
    }

    private void AddMove()
    {
        moves++;
        revealedTiles[0].Reveal(false);
        revealedTiles[1].Reveal(false);
        RevealedTilesCount = 0;
        revealedTiles = [];
        GD.Print(moves);
    }

    private void CheckPair()
    {
        (string name1, string name2) = (revealedTiles[0].TileName, revealedTiles[1].TileName);
        if(name1 == name2)
        {
            score ++;
            revealedTiles[0].SetSolved();
            revealedTiles[1].SetSolved();
            GD.Print("score",score);
        }
    }


    private void OnTileRevealed(MemoryTile memoryTile)
    {
        revealedTiles.Add(memoryTile);
        RevealedTilesCount++;

        if(RevealedTilesCount == 2)
        {
            CheckPair();
            GetTree().CreateTimer(0.6).Timeout += AddMove;
        }
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
        levelSpritesList = [.. spriteAndFrameList.Select(item=>item.Item1.Name)];
        return spriteAndFrameList;
    }

    
    public List<(ImageResource,ImageResource)> SetGameLevel(int levelNumber)
    {
        var levelConfig = LevelConfig[levelNumber];
        gameLevel = levelNumber;
        revealedTiles = [];
        RevealedTilesCount = 0;
        moves = 0;
        score = 0;
        var rows = levelConfig.Row;
        var columns = levelConfig.Column;

        var uniqueSpriteCount = rows*columns/2;
        var spriteAndFrames = GetRandomImages(uniqueSpriteCount);
        return spriteAndFrames;
    }

    
    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        imageManager = GetNode<ImageManager>("/root/ImageManager");
        signalManager.TileRevealed += OnTileRevealed;
        SetLevelConfig();
    }

    public void ClearNodesInGroup(string groupName)
    {
        GetTree().GetNodesInGroup(groupName).ToList().ForEach(node => node.QueueFree());
    }

}
