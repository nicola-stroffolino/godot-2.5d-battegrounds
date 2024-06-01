using Godot;
using System;

public partial class Block : CsgBox3D {
	public AnimationPlayer AnimPlayer { get; set; }
	public Area3D HurtBox { get; set; }

	public override void _Ready() {
		AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		HurtBox = GetNode<Area3D>("HurtBox");
		HurtBox.Connect(Area3D.SignalName.AreaEntered, new Callable(this, MethodName.Destroy));
	}

	public override void _Process(double delta) {

	}

	public void Destroy(Area3D hitBox) {
		GD.Print("aoaoaoa");
		AnimPlayer.Play("break");
	}
}
