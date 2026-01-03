using Godot;
using System;

public partial class FrontSprite : TextureRect
{
    private Vector2 SCALE_MIN = new(0.1f,0.1f);
    private Vector2 SCALE_NORMAL = new(1,1);
    private float MAX_ROTATION = 180;
    private float MAX_DURATION = 1;
    private ImageManager imageManager;

    private float GetRandomRotation()
    {
        var randomRotation = GD.RandRange(0,MAX_ROTATION);
        return (float) Mathf.DegToRad(randomRotation);
    }

    private float GetRandomDuration()
    {
        var randomDuration = (float) GD.RandRange(0, MAX_DURATION);
        return 0.5f + randomDuration;
    }
    private void SetRandomTexture()
    {
        var randomSprite = imageManager.GetRandomImage();
        Texture = randomSprite.Image;
    }
    private void LoadAnimation()
    {
        var tween = CreateTween();
        tween.SetLoops();
            tween.TweenCallback(Callable.From(SetRandomTexture));
            tween.TweenProperty(this, "scale", SCALE_NORMAL, GetRandomDuration());
            tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomDuration());
            tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomDuration());
            tween.TweenProperty(this, "scale", SCALE_MIN, GetRandomDuration());
    }
    public override void _Ready()
    {
        imageManager = GetNode<ImageManager>("/root/ImageManager");
        LoadAnimation();
    }
}
