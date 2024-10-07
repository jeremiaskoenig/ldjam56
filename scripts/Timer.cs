using Godot;
using System;

public partial class Timer : Label
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var seconds = GameState.Instance.CurrentRoundTimer % 60;
        var minutes = (int)(GameState.Instance.CurrentRoundTimer / 60);

        if (seconds < 0)
            seconds = 0;

        if (minutes > 0)
            Text = $"Time: {minutes:0}:{seconds:00}";
        else
            Text = $"Time: {seconds:0.00}";
    }
}
