using Godot;
using System;

public partial class Score : VBoxContainer
{
    private Label movesLabel;
    private Label pairsLabel;
    private Label timeLabel;
    private SignalManager signalManager;
    public override void _Ready()
    {
        signalManager = GetNode<SignalManager>("/root/SignalManager");
        signalManager.UpdateScore += UpdateScore;
        movesLabel = GetNode<Label>("HBMove/Move");
        pairsLabel = GetNode<Label>("HBPair/Pair");
        timeLabel = GetNode<Label>("HBTime/Time");

    }

    public void UpdateScore(int moves, int pairs)
    {
        movesLabel.Text = moves.ToString();
        pairsLabel.Text = pairs.ToString();
    }

    public void UpdateTime(int elapsedTime)
    {
        timeLabel.Text = elapsedTime.ToString() + "s";
    }

}
