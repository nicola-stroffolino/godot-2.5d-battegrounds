using Godot;
using System;

public partial class Player : CharacterBody3D {
	public AnimatedSprite3D Sprite { get; set; }
	public Vector2 SpriteDirection { get; set; } // should NEVER be equal to Vector2.Zero

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();


    public override void _Ready() {
		Sprite = GetNode<AnimatedSprite3D>("%Sprite");
		
		var scale = Sprite.Scale;
		var pos = Sprite.Position;
		var cam = GetViewport().GetCamera3D();
		var cosec = 1 / Mathf.Cos(cam.GlobalRotation.X);
		scale.Y = cosec;
		Sprite.Scale = scale;
		pos.Y = (scale.Y - 1) / 2;
		Sprite.Position = pos;

		SpriteDirection = Vector2.Down;
	}

    public override void _Process(double delta) {

    }

    public override void _PhysicsProcess(double delta) {
		if (!IsOnFloor())
			Velocity = new() {
				X = Velocity.X,
				Y = Velocity.Y - gravity * (float)delta,
				Z = Velocity.Z
			};
			 
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			Velocity = new() {
				X = Velocity.X,
				Y = JumpVelocity,
				Z = Velocity.Z
			};

		MoveAndSlide();
	}

    public override void _Input(InputEvent @event) {
		if (@event is not InputEventKey) return;

		var inputDirection = new Vector2 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		};

		if (inputDirection != Vector2.Zero) {
			SpriteDirection = inputDirection;
			Sprite.Play("walk_" + Game.Directions[SpriteDirection]);
		}
		else Sprite.Play("idle_" + Game.Directions[SpriteDirection]);

		inputDirection = inputDirection.Normalized();

		Vector3 velocity = Velocity;
		if (inputDirection != Vector2.Zero) {
			velocity.X = inputDirection.X * Speed;
			velocity.Z = inputDirection.Y * Speed;
		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
    }
}
