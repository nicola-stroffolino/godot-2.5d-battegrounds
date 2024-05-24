using Godot;
using System;

public partial class Player : CharacterBody3D {
	public Sprite3D Sprite { get; set; }
	public Vector3 LastDirection { get; set; } = Vector3.Down;

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready() {
		Sprite = GetNode<Sprite3D>("%Sprite");
	}

    public override void _PhysicsProcess(double delta) {
		// Vector3 velocity = Velocity;

		// // Add the gravity.
		// if (!IsOnFloor())
		// 	velocity.Y -= gravity * (float)delta;

		// // Handle Jump.
		// if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		// 	velocity.Y = JumpVelocity;

		// // Get the input direction and handle the movement/deceleration.
		// // As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		// if (direction != Vector3.Zero)
		// {
		// 	velocity.X = direction.X * Speed;
		// 	velocity.Z = direction.Z * Speed;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		// 	velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		// }

		// Velocity = velocity;
		// MoveAndSlide();
	}

    public override void _Input(InputEvent @event) {
		var inputDirection = Vector3.Zero;
		inputDirection.X = @event.GetActionStrength("move_right") - @event.GetActionStrength("move_left");
		inputDirection.Z = @event.GetActionStrength("move_down") - @event.GetActionStrength("move_up");

		var lastDirAngle = Mathf.Atan2(LastDirection.Y, LastDirection.X);
		var inputDirAngle = Mathf.Atan2(inputDirection.Y, inputDirection.X);

		var rotationDirection = Mathf.Sign(lastDirAngle - inputDirAngle);
		var steps = rotationDirection * (inputDirAngle / (Mathf.Pi / 4));
		GD.Print(steps);

		LastDirection = inputDirection;
    }
}
