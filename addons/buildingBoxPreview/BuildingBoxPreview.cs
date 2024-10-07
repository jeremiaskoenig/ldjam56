#if TOOLS
using Godot;
using System;

[Tool]
public partial class BuildingBoxPreview : EditorPlugin
{
    //const MyCustomGizmoPlugin = preload("res://addons/my-addon/my_custom_gizmo_plugin.gd");

    private BuildingBoxPreviewGizmo gizmoPlugin = new();

    public override void _EnterTree()
    {
        AddNode3DGizmoPlugin(gizmoPlugin);
    }

    public override void _ExitTree()
    {
        RemoveNode3DGizmoPlugin(gizmoPlugin);
    }
}
#endif
