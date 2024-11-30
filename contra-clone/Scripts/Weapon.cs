using Godot;
using System;

public partial class Weapon : Node2D
{
	// Position to spawn a bullet
	Vector2 weaponPosition;


	// Fire type for different weapons
	// 0 = normal bullets
	int fireType = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleInput();
	}

	void HandleInput()
	{
		if (fireType == 0)
		{
			if (Input.IsActionJustPressed("shoot"))
			{
			}
		}
	}
}
