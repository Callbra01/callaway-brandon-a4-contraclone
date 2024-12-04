using Godot;
using System;

public partial class FireBullet : RigidBody2D
{
    // Called when the node enters the scene tree for the first time.
    float lifeTimeTimer = 0;
    float maxLifeTimer = 5;
    float rotationSpeed = 5;

    [Export]
    public Node2D ballMask;
    [Export]
    public Node2D bulletBalls;

    public override void _Ready()
	{
        bulletBalls.Visible = false;
        ballMask.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        lifeTimeTimer += (float)delta;
        ApplyTorque(rotationSpeed);
        // Destroy object after a given amount of time
        if (lifeTimeTimer > maxLifeTimer)
        {
            QueueFree();
        }

        if (GetCollidingBodies().Count > 0)// && lifeTimeTimer > 0.5f)
        {
            bulletBalls.Visible = true;
            ballMask.Visible = false;
            GD.Print(ContactMonitor);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        
    }

    private void OnBodyEntered(Node2D body)
    {

    }
}
