using Godot;
using System;

public partial class ShotgunBullet : RigidBody2D
{
    float lifeTimeTimer = 0;
    float maxLifeTimer = 1.25f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        lifeTimeTimer += (float)delta;

        // Destroy object after a given amount of time
        if (lifeTimeTimer > maxLifeTimer)
        {
            QueueFree();
        }

        if (GetCollidingBodies().Count > 0)
        {
            QueueFree();
        }

        
    }
}
