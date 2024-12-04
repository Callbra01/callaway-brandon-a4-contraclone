using Godot;
using System;

public partial class BasicBullet : RigidBody2D
{

	VisibleOnScreenNotifier2D onScreen;
	float lifeTimeTimer = 0;
	float maxLifeTimer = 3;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		lifeTimeTimer += (float)delta;

		GD.Print($"YUR {lifeTimeTimer}");
		if (this.GetParent().GetChildCount() > 5)
		{
			QueueFree();
		}
		if (lifeTimeTimer > maxLifeTimer )
		{
			QueueFree();
		}
	}
}
