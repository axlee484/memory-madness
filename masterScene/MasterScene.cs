using Godot;
using System;

public partial class MasterScene : CanvasLayer
{
    private SignalManager signalManager;
    private SoundManager soundManager;
    private GameScene gameScene;
    private MainScene mainScene;
    private AudioStreamPlayer bgmPlayer;


    private void ShowGameScene(bool show)
    {
        mainScene.Visible = show;
        gameScene.Visible = !show;
    }
    private void PlayBGMSound()
    {
        soundManager.PlaySound(bgmPlayer, SoundType.SOUND_MAIN_MENU);
    }
    private void PlayGameSound()
    {
        soundManager.PlaySound(bgmPlayer, SoundType.SOUND_IN_GAME);
    }
    private void OnExitButtonPressed()
    {
        ShowGameScene(true);
        PlayBGMSound();
    }

    public void StartLevel(int levelNumber)
    {
        ShowGameScene(false);
        PlayGameSound();
    }
    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        signalManager.ExitGame += OnExitButtonPressed;
        signalManager.StartLevel += StartLevel;

        soundManager = GetNode<SoundManager>("/root/SoundManager");
        gameScene = GetNode<GameScene>("GameScene");
        mainScene = GetNode<MainScene>("MainScene");
        bgmPlayer = GetNode<AudioStreamPlayer>("BGMPlayer");
        OnExitButtonPressed();

    }
}
