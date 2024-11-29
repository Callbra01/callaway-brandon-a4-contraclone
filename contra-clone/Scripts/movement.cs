using Godot;
using System;

public partial class movement : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;

    [Export]
    public float JumpVelocity = -120.0f;

    [Export]
    public int FallAcceleration { get; set; } = 400;

    private Vector2 _targetVelocity = Vector2.Zero;

    // Physics update loop
    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector2.Zero;

        // Modify velocity based on input
        if (Input.IsActionPressed("move_right"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X -= 1.0f;
        }

        // Update target velocity
        _targetVelocity = direction * Speed;

        // Apply gravity if player is in air, otherwise allow player to jump
        if (!IsOnFloor())
        {
            _targetVelocity.Y += FallAcceleration * 75 * (float)delta;
        }
        else
        {
            //_targetVelocity.Y = 0;
            if (Input.IsActionPressed("jump"))
            {
                _targetVelocity.Y += JumpVelocity * 100;
            }
        }
        GD.Print(_targetVelocity.Y);

        // Apply targetVelocity
        Velocity = _targetVelocity;
        MoveAndSlide();
    }


}
