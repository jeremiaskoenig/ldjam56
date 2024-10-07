using Godot;
using System;

public partial class Creature : CharacterBody3D
{
    private static bool characterSelected = false;
    private bool logY;


    [Export]
    public float Speed { get; set; } = 5.0f;

    [Export]
    public PackedScene Splatter { get; set; }
    
    [Export]
    public float Panic { get; set; } = 1.0f;

    [Export]
    public float PlayerDistance { get; set; } = 5;

    private bool collided = true;
    private double playerScanCooldown = 0;

    public override void _Ready()
    {
        if (!characterSelected)
        {
            characterSelected = true;
            logY = true;
        }

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

            Rotation = Vector3.Zero;
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

        // Fix velocity broken by the scare movement
        Velocity = velocity * new Vector3(1, 0, 1);
        
        collided = MoveAndSlide();

        // Fix position because MoveAndSlide can move vertically (which we don't want)
        Position *= new Vector3(1, 0, 1);

        Vector3? scarePoint = null;

        playerScanCooldown += delta;
        if (playerScanCooldown >= 0.3)
        {
            playerScanCooldown = 0;
            if (GlobalPosition.DistanceTo(CharacterController.Instance.GlobalPosition) <= PlayerDistance)
            {
                scarePoint = CharacterController.Instance.GlobalPosition;
            }
        }

        if (collided)
        {
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                var collision = GetSlideCollision(i);

                var node = (Node3D)collision.GetCollider();
                if (node is CharacterController)
                {
                    Splat();
                }
            }
        }
        else if (scarePoint != null)
        {
            Velocity = (GlobalPosition - scarePoint).Value.Normalized() * (Speed + (Speed * Panic));
        }
    }

    public void Splat()
    {
        if (IsQueuedForDeletion())
            return;

        QueueFree();
        var newNode = Splatter.Instantiate<Node3D>();
        GetParent().AddChild(newNode);
        newNode.GlobalPosition = GlobalPosition - new Vector3(0, Position.Y - 0.001f, 0);
        GameState.Instance.AddScore(5);
    }

}
