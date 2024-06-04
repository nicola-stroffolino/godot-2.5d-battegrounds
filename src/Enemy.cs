using Godot;
using System;

public partial class Enemy : CharacterBody3D {
	public AnimatedSprite3D Sprite { get; set; }
	public Vector2 SpriteDirection { get; set; } = Vector2.Down; // should NEVER be equal to Vector2.Zero
	public Area3D DetectionRange { get; set; }
	public Player Target { get; set; }

	public const float Speed = 2.0f;

    public override void _Ready() {
		Sprite = GetNode<AnimatedSprite3D>("%Sprite");
		DetectionRange = GetNode<Area3D>("DetectionRange");
		DetectionRange.Connect(Area3D.SignalName.BodyEntered, new Callable(this, MethodName.OnAreaEntered));
		
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
		MoveAndSlide();
	}

	public void OnAreaEntered(Node3D body) {
		if (body is not Player p) return;
		Target = p;
	}
}
