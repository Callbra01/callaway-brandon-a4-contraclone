using Godot;
using System;

public partial class movement : CharacterBody2D
{
    [Export]
    public float PlayerSpeed = 500f;

    [Export]
    public const float JumpVelocity = -500f;

    [Export]
    public const float Gravity = 550f;

    // Physics update loop
    public override void _PhysicsProcess(double delta)
    {
        Vector2 _targetVelocity = Velocity;

        // Handle jumping
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            _targetVelocity.Y = JumpVelocity;
        }

        // Handle horizontal velocity
        if (Input.IsActionPressed("move_left"))
        {
            _targetVelocity.X = -PlayerSpeed;
        }
        else if (Input.IsActionPressed("move_right"))
        {
            _targetVelocity.X = PlayerSpeed;
        }
        else
        {
            _targetVelocity.X = 0;
        }

        // Apply gravity if player is in air
        if (!IsOnFloor())
        {
            _targetVelocity.Y += Gravity * (float)delta;
        }

        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
