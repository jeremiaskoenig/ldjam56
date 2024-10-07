using Godot;
using System;

public partial class AnimateUV : MeshInstance3D
{
    [Export]
    public float FrameLength { get; set; }

    [Export]
    public Vector2[] UVOffsets { get; set; }

    [Export]
    public StandardMaterial3D AnimatedMaterial { get; set; }

    double currentTotal = 0;

    public override void _Process(double delta)
    {
        currentTotal += delta;

        var offset = currentTotal % (FrameLength * UVOffsets.Length);
        var frame = (int)(offset / FrameLength);

        var uvOffset = UVOffsets[frame];
        AnimatedMaterial.Uv1Offset = new(uvOffset.X, uvOffset.Y, 0);
    }
}
