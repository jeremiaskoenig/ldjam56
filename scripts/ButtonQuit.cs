using Godot;
using System;

public partial class ButtonQuit : Button
{
    public override void _Pressed()
    {
        GetTree().Quit();
    }
}
