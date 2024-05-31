using Godot;
using System;

public partial class Player : CharacterBody3D {
	public AnimatedSprite3D Sprite { get; set; }

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
	}

    public override void _PhysicsProcess(double delta) {
		MoveAndSlide();
	}
}
