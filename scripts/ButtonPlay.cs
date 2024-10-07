using Godot;
using System;

public partial class ButtonPlay : Button
{
    [Export]
    UserInterface UI { get; set; }

    public override void _Pressed()
    {
        GameState.Instance.RestartGame();
        UI.ToggleUI(UserInterface.UIView.Ingame);
    }
}
