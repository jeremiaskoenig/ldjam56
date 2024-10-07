using Godot;
using System;

public partial class Spawner : Node3D
{
    [Export]
    public int SpawnCount { get; set; }

    [Export]
    public PackedScene SpawnedCreature { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        Spawn();
    }

    private void Spawn()
    {
        if (IsQueuedForDeletion())
            return;

        var parentNode = GetParent();
        for (int i = 0; i < SpawnCount; i++)
        {
            var creature = SpawnedCreature.Instantiate<Node3D>();
            parentNode.AddChild(creature);
            creature.GlobalPosition = GlobalPosition;
        }
        QueueFree();
    }
}
