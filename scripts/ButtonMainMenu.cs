using Godot;
using System;

public partial class ButtonMainMenu : Button
{
    [Export]
    UserInterface UI { get; set; }

    public override void _Pressed()
    {
        UI.ToggleUI(UserInterface.UIView.MainMenu);
    }
}
