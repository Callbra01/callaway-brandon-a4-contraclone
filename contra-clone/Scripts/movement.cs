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

    [Export]
    private PackedScene BasicBulletPrefab;

    [Export]
    Node2D weapon;

    CollisionShape2D jumpCollisionShape;

    public bool playerFacingRight = true;

    Vector2 weaponPosition = new Vector2(60, -18);

    // Vars for platform traversal
    bool isPhasing = false;
    float phaseTimer = 0;

    public override void _Ready()
    {
        weapon = GetNode<Node2D>("Weapon");
        jumpCollisionShape = GetNode<CollisionShape2D>("JumpCollisionBox");
    }

    public override void _Process(double delta)
    {
        // TODO: Swap sprite and weapon position side
        if (playerFacingRight)
        {
            weapon.Position = weaponPosition;
        }
        else
        {
            weapon.Position = new Vector2(-weaponPosition.X, weaponPosition.Y);
        }

        HandleJumpCollisionBox(delta);

        if (Input.IsActionJustPressed("shoot"))
        {
            RigidBody2D bullet = BasicBulletPrefab.Instantiate<RigidBody2D>();
            weapon.AddChild(bullet);
            bullet.GlobalPosition = weapon.GlobalPosition;
            bullet.ApplyForce(new Vector2(15000, 0));
            
        }
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

    void HandleJumpCollisionBox(double delta)
    {
        // If player is traveling through a platform, disable jump collision shape, for a given amount of time
        if (isPhasing && phaseTimer < 0.5f)
        {
            jumpCollisionShape.Disabled = true;
            phaseTimer += (float)delta;
        }
        else
        {
            jumpCollisionShape.Disabled = false;
            isPhasing = false;
            ResetPhaseTimer();
        }
    }

    void ResetPhaseTimer()
    {
        phaseTimer = 0;
    }
}
