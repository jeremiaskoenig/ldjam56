using Godot;
using System;

public partial class ScoreLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.Initialized += () =>
		{
			GameState.Instance.ScoreChanged += UpdateScore;
		};
	}

	private void UpdateScore(int newScore)
	{
		Text = $"Score: {newScore}";
	}
}
