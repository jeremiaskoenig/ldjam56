using Godot;
using System;

public partial class Building : Node3D
{
    [Export]
    public int SpawnCount { get; set; }

    [Export]
    public PackedScene SpawnedCreature { get; set; }

    [Export]
    public PackedScene Explosion { get; set; }

    [Export]
    public PackedScene Rubble { get; set; }

    //[Export]
    //public int BuildingHP { get; set; }

    public void Spawn()
    {
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
    }
}
