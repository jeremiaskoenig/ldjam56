using Godot;
using System;

public partial class Splatter : Node3D
{
    public override void _Ready()
    {
        RotateY(Mathf.DegToRad(360 * GD.Randf()));
    }
}
