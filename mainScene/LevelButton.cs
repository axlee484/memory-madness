using Godot;
using System;

public partial class LevelButton : TextureButton
{
    public int LevelNumber {get; set;}
    private GameManager gameManager;
    private SoundManager soundManager;
    private AudioStreamPlayer audioStreamPlayer;
    private SignalManager signalManager;
    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        soundManager = GetNode<SoundManager>("/root/SoundManager");
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        var label = GetNode<Label>("Label");
        var levelConfig = gameManager.LevelConfig[LevelNumber];
        label.Text = $"{levelConfig.Row}x{levelConfig.Column}";
    }

    private void PlayPressedSound()
    {
        soundManager.PlaySound(audioStreamPlayer, SoundType.SOUND_SELECT_BUTTON);
    }
    public void OnPressed()
    {
        PlayPressedSound();
        signalManager.EmitSignal(SignalManager.SignalName.StartLevel, LevelNumber);        
    }
}
