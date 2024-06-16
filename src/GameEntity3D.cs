using Godot;

public partial class GameEntity3D : CharacterBody3D {
    // public AnimatedSprite3D Sprite { get; set; }
	// public Sprite3D LeftHand { get; set; }
	// public Sprite3D RightHand { get; set; }
	public Vector2 SpriteDirection { get; set; } = Vector2.Down; // should NEVER be equal to Vector2.Zero
    public Vector2 MoveDirection { get; set; } = Vector2.Zero;

    private Vector3 _velocity = Vector3.Zero;
    public float Speed { get; set; } = 5.0f;

    public override void _Ready() {
		// Sprite = GetNode<AnimatedSprite3D>("%Sprite");
		// LeftHand = GetNode<Sprite3D>("LeftHand");
		// RightHand = GetNode<Sprite3D>("RightHand");

		// var scale = Sprite.Scale;
		// var pos = Sprite.Position;
		// var cam = GetViewport().GetCamera3D();
		// var cosec = 1 / Mathf.Cos(cam.GlobalRotation.X);

		// scale.Y = cosec;
		// pos.Y = (scale.Y - 1) / 2;

		// Sprite.Scale = scale;
		// LeftHand.Scale = scale;
		// RightHand.Scale = scale;

		// Sprite.Position = pos;

		// Sprite = ScaleAndReposition(Sprite);
		// LeftHand = ScaleAndReposition(LeftHand);
		// RightHand = ScaleAndReposition(RightHand);
		
		// Sprite.Billboard = BaseMaterial3D.BillboardModeEnum.FixedY;
		// LeftHand.Billboard = BaseMaterial3D.BillboardModeEnum.FixedY;
		// RightHand.Billboard = BaseMaterial3D.BillboardModeEnum.FixedY;
	}

    public override void _PhysicsProcess(double delta) {
		// Velocity = new() {
		// 	X = Mathf.Lerp(Velocity.X, _velocity.X, (float)delta * 10),
		// 	Z = Mathf.Lerp(Velocity.Z, _velocity.Z, (float)delta * 10),
		// };

		MoveAndSlide();
	}

    public virtual bool WantsToMove() => false;
    public virtual bool WantsToAttack() => false;

	public T ScaleAndReposition<T>(T sprite) where T: Node3D {
		var scale = sprite.Scale;
		var pos = sprite.Position;
		
		var cam = GetViewport().GetCamera3D();
		var cosec = 1 / Mathf.Cos(cam.GlobalRotation.X);
		
		scale.Y = cosec;
		pos.Y = (scale.Y - 1) / 2;

		sprite.Scale = scale;
		sprite.Position = pos;

		return sprite;
	}
}