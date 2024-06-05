using Godot;
using System;

public partial class Enemy : GameEntity3D {
	public Area3D DetectionRange { get; set; }
	public Player Target { get; set; }

    public override void _Ready() {
		base._Ready();
		DetectionRange = GetNode<Area3D>("DetectionRange");
		DetectionRange.Connect(Area3D.SignalName.BodyEntered, new Callable(this, MethodName.OnAreaEntered));
	}

	public void OnAreaEntered(Node3D body) {
		if (body is not Player p) return;
		Target = p;
	}
}
