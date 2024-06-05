using Godot;

public partial class GameEntity3D : CharacterBody3D {
    public AnimatedSprite3D Sprite { get; set; }
	public Vector2 SpriteDirection { get; set; } = Vector2.Down; // should NEVER be equal to Vector2.Zero
    public Vector2 MoveDirection { get; set; } = Vector2.Zero;

    private Vector3 _velocity = Vector3.Zero;
    public const float Speed = 5.0f;

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
		Sprite.Billboard = BaseMaterial3D.BillboardModeEnum.FixedY;
	}

    public override void _PhysicsProcess(double delta) {
		Velocity = new() {
			X = Mathf.Lerp(Velocity.X, _velocity.X, (float)delta * 10),
			Z = Mathf.Lerp(Velocity.Z, _velocity.Z, (float)delta * 10),
		};

		MoveAndSlide();
	}

    public Vector3 GetVelocity() => _velocity;
	public void SetVelocity(Vector3 value) => _velocity = value;

    public virtual bool WantsToMove() => false;
    public virtual bool WantsToAttack() => false;
}