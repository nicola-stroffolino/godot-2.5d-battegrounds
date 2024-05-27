using Godot;
using System;

public partial class Player : CharacterBody3D {
	public AnimatedSprite3D Sprite { get; set; }
	public Vector2 SpriteDirection { get; set; } // should NEVER be equal to Vector2.Zero
	// public MeshInstance3D Shadow { get; set; }
	// public RayCast3D GroundRay { get; set; }

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();


    public override void _Ready() {
		Sprite = GetNode<AnimatedSprite3D>("%Sprite");
		// Sprite.Scale = new() {
		// 	X = Sprite.Scale.X,
		// 	Y = Sprite.Scale.Y * Mathf.Sqrt(2),
		// 	Z = Sprite.Scale.Z,
		// };
		SpriteDirection = Vector2.Down;
		// Shadow = GetNode<MeshInstance3D>("%Shadow");

		// Sprite.Billboard = BaseMaterial3D.BillboardModeEnum.Enabled;
	}

    public override void _Process(double delta) {
		// ShadowShader.SetShaderParameter("player_position", GlobalPosition);
		// var shaderMaterial = (ShaderMaterial)Shadow.Mesh.SurfaceGetMaterial(0);
		// var rayCollisionHeight = GroundRay.GetCollisionPoint().Y;
		// shaderMaterial.SetShaderParameter("distance_from_ground", Shadow.GlobalPosition.Y - rayCollisionHeight);

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
