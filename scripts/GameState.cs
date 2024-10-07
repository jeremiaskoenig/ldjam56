using Godot;
using System;

public partial class GameState : Node3D
{
	public static event Action Initialized;

	public static GameState Instance { get; private set; }

	public int Score { get; private set; }
	public int HighScore { get; private set; }

	public event Action<int> ScoreChanged;
	public event Action<int> GameOver;

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr($"Duplicate GameState. Source: {Name}");

		LoadHighScore();

		Initialized?.Invoke();
	}

	private void LoadHighScore()
	{
		if (FileAccess.FileExists("user://highscore.dat"))
		{
			using var file = FileAccess.Open("user://highscore.dat", FileAccess.ModeFlags.Read);
			HighScore = (int)file.Get32();
		}
	}

	[Export]
	public Node3D GameContainer { get; set; }

	[Export]
	public int RoundLength { get; set; } = 300;

	[Export]
	public PackedScene Level { get; set; }
	private Node3D currentLevel;

	[Export]
	public Vector3 PlayerSpawn { get; set; }

	public double CurrentRoundTimer { get; set; }
	public bool IsRoundRunning { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		if (IsRoundRunning)
			CurrentRoundTimer -= delta;

		if (CurrentRoundTimer < 0)
		{
			IsRoundRunning = false;
			ProcessMode = ProcessModeEnum.Disabled;

			if (Score > HighScore)
			{
				HighScore = Score;

				using var file = FileAccess.Open("user://highscore.dat", FileAccess.ModeFlags.Write);
				if (file == null)
				{
					GD.PrintErr(FileAccess.GetOpenError());
				}
				else
					file.Store32((uint)Score);
			}

			GameOver?.Invoke(Score);
		}
	}

	public void RestartGame()
	{
		currentLevel?.Free();
		currentLevel = Level.Instantiate<Node3D>();
		GameContainer.AddChild(currentLevel);
		Score = 0;

		(GameContainer.FindChild("Player") as Node3D).Position = PlayerSpawn;

		CurrentRoundTimer = RoundLength;
		IsRoundRunning = true;
		ProcessMode = ProcessModeEnum.Inherit;
	}

	public void AddScore(int value)
	{
		Score += value;
		ScoreChanged?.Invoke(Score);
	}
}
