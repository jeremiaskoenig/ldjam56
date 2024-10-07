using Godot;
using System;
using System.Text;

public partial class UserInterface : Control
{
    public enum UIView
    {
        Ingame,
        MainMenu,
        GameOver,
        Credits

    }

    [Export]
    public Control IngameView { get; set; }
    [Export]
    public Control MainMenuView { get; set; }
    [Export]
    public Control GameOverView { get; set; }
    [Export]
    public Control CreditsView { get; set; }

    [Export]
    public Label GameOverScoreboard { get; set; }

    public override void _Ready()
    {
        GameState.Initialized += () =>
        {
            GameState.Instance.GameOver += finalScore =>
            {
                StringBuilder builder = new();
                builder.AppendLine("Final Score:");
                builder.AppendLine(finalScore.ToString());
                builder.AppendLine();
                builder.AppendLine("High Score:");
                builder.AppendLine(GameState.Instance.HighScore.ToString());

                GameOverScoreboard.Text = builder.ToString();
                ToggleUI(UIView.GameOver);
            };
        };
    }

    public void ToggleUI(UIView view)
    {
        GD.Print($"Change view to {view}");
        switch (view)
        {
            case UIView.Ingame:
                IngameView.Visible = true;
                MainMenuView.Visible = false;
                MainMenuView.ProcessMode = ProcessModeEnum.Disabled;
                GameOverView.Visible = false;
                GameOverView.ProcessMode = ProcessModeEnum.Disabled;
                CreditsView.Visible = false;
                CreditsView.ProcessMode = ProcessModeEnum.Disabled;
                break;
            case UIView.MainMenu:
                IngameView.Visible = false;
                MainMenuView.Visible = true;
                MainMenuView.ProcessMode = ProcessModeEnum.Always;
                GameOverView.Visible = false;
                GameOverView.ProcessMode = ProcessModeEnum.Disabled;
                CreditsView.Visible = false;
                CreditsView.ProcessMode = ProcessModeEnum.Disabled;
                break;
            case UIView.GameOver:
                IngameView.Visible = false;
                MainMenuView.Visible = false;
                MainMenuView.ProcessMode = ProcessModeEnum.Disabled;
                GameOverView.Visible = true;
                GameOverView.ProcessMode = ProcessModeEnum.Always;
                CreditsView.Visible = false;
                CreditsView.ProcessMode = ProcessModeEnum.Disabled;
                break;
            case UIView.Credits:
                IngameView.Visible = false;
                MainMenuView.Visible = false;
                MainMenuView.ProcessMode = ProcessModeEnum.Disabled;
                GameOverView.Visible = false;
                GameOverView.ProcessMode = ProcessModeEnum.Disabled;
                CreditsView.Visible = true;
                CreditsView.ProcessMode = ProcessModeEnum.Always;
                break;
        }
    }

}
