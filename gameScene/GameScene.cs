using Godot;
using System;

public partial class GameScene : Control
{
    private SignalManager signalManager;
    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
    }
    public void OnExitButtonPressed()
    {
        signalManager.EmitSignal(SignalManager.SignalName.ExitGame);
    }
}
