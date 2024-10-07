using Godot;
using System;

public partial class ButtonCredits : Button
{
    [Export]
    UserInterface UI { get; set; }

    public override void _Pressed()
    {
        UI.ToggleUI(UserInterface.UIView.Credits);
    }
}
