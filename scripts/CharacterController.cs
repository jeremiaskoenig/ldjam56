using Godot;
using System;

public partial class CharacterController : CharacterBody3D
{
	private const string CreaturePrefix = "Creature";
	private const string BuildingPrefix = "Building";

	[Export]
	public float MoveSpeed { get; set; } = 50f;

	[Export]
	public float TurnSpeed { get; set; } = 0.5f;

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Vector3.Zero;

		if (Input.IsKeyPressed(Key.W))
		{
			velocity = Vector3.Forward.Rotated(Vector3.Up, Rotation.Y) * MoveSpeed;
		}
		else if (Input.IsKeyPressed(Key.S))
		{
			velocity = Vector3.Forward.Rotated(Vector3.Up, Rotation.Y) * -MoveSpeed;
		}
		else
		{
			velocity.Z = 0;
		}
		
		if (Input.IsKeyPressed(Key.A))
		{
			RotateY(TurnSpeed);
		}
		else if (Input.IsKeyPressed(Key.D))
		{
			RotateY(-TurnSpeed);
		}

		Velocity = velocity;
		if (MoveAndSlide())
		{
			GD.Print("Player Collide");
			for (int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				var node = (Node3D)collision.GetCollider();
				GD.Print($"With {node.Name}");
				switch (node)
				{
					case Building building:
						GD.Print("Is Building");
						building.Spawn();
						break;
					case Creature creature:
						GD.Print("Is Creature");
						creature.Splat();
						break;
				}
			}
		}
	}
}
