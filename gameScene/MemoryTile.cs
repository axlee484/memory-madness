using Godot;
using System;

public partial class MemoryTile : TextureButton
{
    private Control imageContainer;
    private GameManager gameManager;
    private SignalManager signalManager;
    public string TileName {get; set;}
    public int Index {get; set;}
    public bool isVisible;
    public bool isSolved;
    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        imageContainer = GetNode<Control>("ImageContainer");
        isVisible = false;
        isSolved = false;
        Reveal(isVisible);
    }
    public void Reveal(bool show)
    {
        if(isSolved) return;
        imageContainer.Visible = show;
        isVisible = show;
    }

    public void SetSolved()
    {
        isSolved = true;
        Disabled = true;
    }


    public void OnPressed()
    {
        if(gameManager.RevealedTilesCount >= 2) return;
        if (isVisible) return;
        if(isSolved) return;
        Reveal(true);
        signalManager.EmitSignal(SignalManager.SignalName.TileRevealed, this);

    }

    
}
