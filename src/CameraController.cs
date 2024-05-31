using Godot;
using System;

public partial class CameraController : Node3D {
	public Camera3D Camera { get; set; }
	public Node3D FollowTarget { get; set; }

	public override void _Ready() {
		Camera = GetNode<Camera3D>("%Camera");
		FollowTarget = GetTree().CurrentScene.GetNode<Node3D>("%Player");
	}

	public override void _PhysicsProcess(double delta) {
		Position = new Vector3(
            (float)Mathf.Lerp(Position.X, FollowTarget.Position.X, delta * 10),
			FollowTarget.Position.Y,
            (float)Mathf.Lerp(Position.Z, FollowTarget.Position.Z, delta * 10)
		);
	}
}
