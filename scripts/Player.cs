using Godot;
using System;

public partial class Player : CharacterBody3D {
	public AnimatedSprite3D Sprite { get; set; }
	public Vector2 InputDirection { get; set; }

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready() {
		Sprite = GetNode<AnimatedSprite3D>("%Sprite");
		InputDirection = Vector2.Zero;
	}

    public override void _Process(double delta) {
        
    }

    public override void _PhysicsProcess(double delta) {

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

		MoveAndSlide();
	}

    public override void _Input(InputEvent @event) {
		if (@event is not InputEventKey) return;

		InputDirection = new Vector2 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		};

		if (InputDirection == Vector2.Zero) return;

		// int frame = 0;
		// switch(InputDirection) {
		// 	case (0, -1): frame = 0; break;
		// 	case (1, -1): frame = 1; break;
		// 	case (1, 0): frame = 2; break;
		// 	case (1, 1): frame = 3; break;
		// 	case (0, 1): frame = 4; break;
		// 	case (-1, 1): frame = 5; break;
		// 	case (-1, 0): frame = 6; break;
		// 	case (-1, -1): frame = 7; break;
		// }
		// Sprite.FrameCoords = new Vector2I(frame, Sprite.FrameCoords.Y);

		InputDirection = InputDirection.Normalized();
		Vector3 velocity = Velocity;
		if (InputDirection != Vector2.Zero) {
			velocity.X = InputDirection.X * Speed;
			velocity.Z = InputDirection.Y * Speed;
		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}
		Velocity = velocity;
    }
}
