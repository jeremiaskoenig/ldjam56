using Godot;
using System;

public partial class Creature : CharacterBody3D
{
    private static string PlayerName = "Player";

    [Export]
    public float Speed { get; set; } = 5.0f;

    [Export]
    public PackedScene Splatter { get; set; }
    
    [Export]
    public float Panic { get; set; } = 1.0f;

    private bool collided = true;

    public override void _Ready()
    {
        var animPlayer = GetNode<AnimationPlayer>("character-male-a/AnimationPlayer");
        animPlayer.AnimationFinished += (a) => animPlayer.Play("walk");
        if (animPlayer != null)
            animPlayer.Play("walk");
        else
            GD.Print("anim-player null");
    }



    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        if (collided)
        {
            float direction = Mathf.DegToRad(360 * GD.Randf());

            //Rotation = Vector3.Zero;
            RotateY(direction);
            velocity = Vector3.Forward.Rotated(Vector3.Up, direction) * (Speed + (Speed * Panic));
        }

        if (Panic > 0)
        {
            Panic *= 0.6f;

            if (Panic < 0.1)
            {
                Panic = 0;
            }
        }

        Velocity = velocity;
        collided = MoveAndSlide();

        if (collided)
        {
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);

                var node = (Node3D)collision.GetCollider();
                if (node.Name == PlayerName)
                {
                    Splat();
                }
            }
        }
    }

    public void Splat()
    {
        var newNode = Splatter.Instantiate<Node3D>();
        GetParent().AddChild(newNode);
        newNode.GlobalPosition = GlobalPosition - new Vector3(0, Position.Y - 0.001f, 0);
        QueueFree();
    }

}
