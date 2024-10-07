using Godot;
using System;

public partial class GameState : Node3D
{
    public static event Action Initialized;

    public static GameState Instance { get; private set; }

    public int Score { get; private set; }

    public event Action<int> ScoreChanged;

    public override void _Ready()
    {
        if (Instance == null)
            Instance = this;
        else
            GD.PrintErr($"Duplicate GameState. Source: {Name}");

        Initialized?.Invoke();
    }

    public void AddScore(int value)
    {
        Score += value;
        ScoreChanged?.Invoke(Score);
    }
}
