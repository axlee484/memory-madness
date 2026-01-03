using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public record ImageResource(string Name, CompressedTexture2D Image);
public partial class ImageManager : Node
{
    private List<ImageResource> imageData;
    public IReadOnlyList<ImageResource> ImageData => imageData;
    private readonly List<ImageResource> frameImages = [];


    private void LoadImageData()
    {
        imageData = [];
        var path = "res://assets/glitch";
        var dir = DirAccess.Open(path) ?? throw new Exception("Failed to open directory");
        dir.GetFiles()
        .Where(fileName=>!fileName.Contains(".import"))
        .ToList()
        .ForEach(fileName =>
        {
           var imageResource = new ImageResource(fileName.TrimSuffix(".png"), GD.Load<CompressedTexture2D>(path+"/"+fileName));
           imageData.Add(imageResource);
        } 
        );

        var framePath = "res://assets/frames";
        var frameDir = DirAccess.Open(framePath) ?? throw new Exception("Failed to open directory");
        frameDir.GetFiles()
        .Where(fileName=>!fileName.Contains(".import")&&!fileName.Contains("hidden"))
        .ToList()
        .ForEach(fileName =>
        {
           var imageResource = new ImageResource(fileName.TrimSuffix(".png"), GD.Load<CompressedTexture2D>(framePath+"/"+fileName));
           frameImages.Add(imageResource);
        } 
        );
        
    }
    public ImageResource GetImage(int i) => imageData.Count == 0 ?null :imageData[i];
    public int GetImageCount() => imageData.Count;
    public override void _Ready()
    {
        LoadImageData();
    }

    public ImageResource GetRandomFrame() => frameImages[GD.RandRange(0, frameImages.Count-1)];
}
