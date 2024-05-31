using Godot;
using System;

public partial class Block : Node3D {
	public AnimationPlayer AnimPlayer { get; set; }

	public override void _Ready() {
		AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		// Connect(Area3D.SignalName.BodyEntered, new Callable(this, ));
	}

	public override void _Process(double delta) {

	}

	public void Destroy() {
		GD.Print("aoaoaoa");
	}
}
