using Godot;
using System;

[Tool]
public partial class Building : Node3D
{
    private PackedScene buildingVisual;
    private Vector3 hitboxSize;
    private Vector3 hitboxOffset;

    [Export]
    public int SpawnCount { get; set; }

    [Export]
    public int BuildingScoreValue { get; set; } = 15;

    [Export]
    public PackedScene SpawnedCreature { get; set; }

    [Export]
    public PackedScene Explosion { get; set; }

    [Export]
    public PackedScene Rubble { get; set; }

    [Export]
    public PackedScene BuildingVisual
    {
        get => buildingVisual;
        set
        {
            buildingVisual = value; 
            UpdateGizmos();
            CreateVisuals();
        }
    }

    [Export]
    public Vector3 BuildingOffset
    {
        get => buildingOffset;
        set
        {
            buildingOffset = value; 
            UpdateGizmos();
            CreateVisuals();
        }
    }

    [Export]
    public Vector3 HitboxSize
    {
        get => hitboxSize;
        set 
        {
            hitboxSize = value; 
            UpdateGizmos();
            CreateVisuals();
        }
    }
    [Export]
    public Vector3 HitboxOffset
    {
        get => hitboxOffset;
        set
        {
            hitboxOffset = value;  
            UpdateGizmos();
            CreateVisuals();
        }
    }

    [Export]
    public CollisionShape3D Collider { get; set; }

    private Node3D buildingVisualPreview = null;
    private Vector3 buildingOffset;

    public override void _Ready()
    {
        CreateVisuals();
    }
    
    void CreateCollider()
    {
        var collider = new CollisionShape3D
        {
            Shape = new BoxShape3D()
        };
        AddChild(collider);
        Collider = collider;
    }

    void CreateVisuals()
    {
        if (Collider == null)
            CreateCollider();

        Collider.Position = HitboxOffset;
        var collisionBox = Collider.Shape as BoxShape3D;
        collisionBox.Size = HitboxSize;

        if (buildingVisualPreview != null)
            buildingVisualPreview.Free();

        buildingVisualPreview = BuildingVisual.Instantiate<Node3D>();
        AddChild(buildingVisualPreview);
        buildingVisualPreview.Position = BuildingOffset;
    }

    public void Spawn()
    {
        if (IsQueuedForDeletion())
            return;
            
        for (int i = 0; i < SpawnCount; i++)
        {
            var creature = SpawnedCreature.Instantiate<Node3D>();
            GetParent().AddChild(creature);
            creature.GlobalPosition = GlobalPosition;
        }
        var explosion = Explosion.Instantiate<Node3D>();
        GetParent().AddChild(explosion);
        explosion.GlobalPosition = GlobalPosition;
        var rubble = Rubble.Instantiate<Node3D>();
        GetParent().AddChild(rubble);
        rubble.GlobalPosition = GlobalPosition;
        QueueFree();
        GameState.Instance.AddScore(BuildingScoreValue);
    }
}
