using Godot;
using System;
using System.Collections.Generic;


public enum SoundType
{
    SOUND_MAIN_MENU,
    SOUND_IN_GAME,
    SOUND_SUCCESS,
    SOUND_GAME_OVER,
    SOUND_SELECT_TILE,
    SOUND_SELECT_BUTTON
}
public partial class SoundManager : Node
{
    public IReadOnlyDictionary<SoundType, AudioStream> Sounds => _Sounds;
    private readonly Dictionary<SoundType, AudioStream> _Sounds = [];
    private void SetSound()
    {
        _Sounds.Add(SoundType.SOUND_MAIN_MENU, GD.Load<AudioStream>("res://assets/sounds/bgm_action_3.mp3"));
        _Sounds.Add(SoundType.SOUND_IN_GAME, GD.Load<AudioStream>("res://assets/sounds/bgm_action_4.mp3"));
        _Sounds.Add(SoundType.SOUND_SUCCESS, GD.Load<AudioStream>("res://assets/sounds/sfx_sounds_fanfare3.wav"));
        _Sounds.Add(SoundType.SOUND_GAME_OVER, GD.Load<AudioStream>("res://assets/sounds/sfx_sounds_powerup12.wav"));
        _Sounds.Add(SoundType.SOUND_SELECT_TILE, GD.Load<AudioStream>("res://assets/sounds/sfx_sounds_impact1.wav"));
        _Sounds.Add(SoundType.SOUND_SELECT_BUTTON, GD.Load<AudioStream>("res://assets/sounds/sfx_sounds_impact7.wav"));

    }
    public override void _Ready()
    {
        SetSound();
    }

    public void PlaySound(AudioStreamPlayer player, SoundType soundType)
    {
        player.Stop();
        player.Stream = Sounds[soundType];
        player.Play();
    }
}
