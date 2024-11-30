using Godot;
using System;

public partial class movement : CharacterBody2D
{
    [Export]
    public float PlayerSpeed = 50f;

    [Export]
    public float JumpVelocity = 50f;

    [Export]
    public float Gravity = 150f;

    public bool playerFacingRight = true;

    Node2D weapon;
    CollisionShape2D bottomCollisionShape;
    CollisionShape2D topCollisionShape;
    Vector2 weaponPosition = new Vector2(60, -18);

    bool isPhasing = false;
    float phaseTimer = 0;


    public override void _Ready()
    {
        weapon = GetNode<Node2D>("Weapon");
        bottomCollisionShape = GetNode<CollisionShape2D>("JumpCollisionBox");
    }

    public override void _Process(double delta)
    {
        if (playerFacingRight)
        {
            weapon.Position = weaponPosition;
        }
        else
        {
            weapon.Position = new Vector2(-weaponPosition.X, weaponPosition.Y);
        }

        HandleCollisionBoxes(delta);
    }

    void HandleCollisionBoxes(double delta)
    {
        if (isPhasing && phaseTimer < 0.5f)
        {
            bottomCollisionShape.Disabled = true;
            phaseTimer += (float)delta;
        }
        else
        {
            bottomCollisionShape.Disabled = false;
            isPhasing = false;
            ResetPhaseTimer();
        }

        GD.Print(bottomCollisionShape.Disabled);
    }

    void ResetPhaseTimer()
    {
        phaseTimer = 0;
    }

    // Physics update loop
    public override void _PhysicsProcess(double delta)
    {
        Vector2 _targetVelocity = Velocity;
        // Handle jumping
        if (IsOnFloor())
        {
            if (Input.IsActionPressed("move_down") && Input.IsActionJustPressed("jump"))
            {
                isPhasing = true;
            }
            else if (Input.IsActionJustPressed("jump"))
            {
                _targetVelocity.Y = -JumpVelocity * 10;
                isPhasing = true;
            }
        }


        // Handle horizontal velocity
        if (Input.IsActionPressed("move_left"))
        {
            _targetVelocity.X = -PlayerSpeed * 10;
            playerFacingRight = false;
        }
        else if (Input.IsActionPressed("move_right"))
        {
            _targetVelocity.X = PlayerSpeed * 10;
            playerFacingRight = true;
        }
        else
        {
            _targetVelocity.X = 0;
        }

        // Apply gravity if player is in air
        if (!IsOnFloor())
        {
            _targetVelocity.Y += (Gravity * 10) * (float)delta;
            // TODO: Fix this rotation for jumping (Player shifts left and right on floor)
            //Rotate(_targetVelocity.X * (float)delta * 0.01f);
        }
        else
        {
            //Rotation = 0;
        }

        // Apply velocity
        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
