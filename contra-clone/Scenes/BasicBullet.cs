using Godot;
using System;

public partial class BasicBullet : RigidBody2D
{

	VisibleOnScreenNotifier2D onScreen;
	float lifeTimeTimer = 0;
	float maxLifeTimer = 3;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		lifeTimeTimer += (float)delta;

		// Destroy object after a given amount of time
		if (lifeTimeTimer > maxLifeTimer )
		{
			QueueFree();
		}
	}
}
