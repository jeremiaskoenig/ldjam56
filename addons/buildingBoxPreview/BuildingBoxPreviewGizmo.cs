#if TOOLS
using Godot;
using Godot.NativeInterop;
using System;

[Tool]
public partial class BuildingBoxPreviewGizmo : EditorNode3DGizmoPlugin
{
    public BuildingBoxPreviewGizmo()
    {
        CreateMaterial("main", new Color(1, 0, 1));
    }

    public override string _GetGizmoName()
    {
        return "BuildingPreview";
    }

    public override bool _HasGizmo(Node3D node3D)
    {
        return node3D is Building;
    }

    public override void _Redraw(EditorNode3DGizmo gizmo)
    {
        gizmo.Clear();

        if (gizmo.GetNode3D() is not Building building)
            return;

        var xMin = building.HitboxOffset.X - (building.HitboxSize.X / 2f);
        var xMax = building.HitboxOffset.X + (building.HitboxSize.X / 2f);

        var yMin = building.HitboxOffset.Y - (building.HitboxSize.Y / 2f);
        var yMax = building.HitboxOffset.Y + (building.HitboxSize.Y / 2f);

        var zMin = building.HitboxOffset.Z - (building.HitboxSize.Z / 2f);
        var zMax = building.HitboxOffset.Z + (building.HitboxSize.Z / 2f);

        var lines = new Vector3[24];

        // Bottom edges
        lines[0] = new Vector3(xMin, yMin, zMin);
        lines[1] = new Vector3(xMax, yMin, zMin);

        lines[2] = new Vector3(xMax, yMin, zMin);
        lines[3] = new Vector3(xMax, yMax, zMin);

        lines[4] = new Vector3(xMax, yMax, zMin);
        lines[5] = new Vector3(xMin, yMax, zMin);

        lines[6] = new Vector3(xMin, yMax, zMin);
        lines[7] = new Vector3(xMin, yMin, zMin);

        // Top edges
        lines[8] = new Vector3(xMin, yMin, zMax);
        lines[9] = new Vector3(xMax, yMin, zMax);

        lines[10] = new Vector3(xMax, yMin, zMax);
        lines[11] = new Vector3(xMax, yMax, zMax);

        lines[12] = new Vector3(xMax, yMax, zMax);
        lines[13] = new Vector3(xMin, yMax, zMax);

        lines[14] = new Vector3(xMin, yMax, zMax);
        lines[15] = new Vector3(xMin, yMin, zMax);

        // Vertical edges
        lines[16] = new Vector3(xMin, yMin, zMin);
        lines[17] = new Vector3(xMin, yMin, zMax);

        lines[18] = new Vector3(xMax, yMin, zMin);
        lines[19] = new Vector3(xMax, yMin, zMax);

        lines[20] = new Vector3(xMax, yMax, zMin);
        lines[21] = new Vector3(xMax, yMax, zMax);

        lines[22] = new Vector3(xMin, yMax, zMin);
        lines[23] = new Vector3(xMin, yMax, zMax);
        
        gizmo.AddLines(lines, GetMaterial("main", gizmo), false);
    }
}
#endif
