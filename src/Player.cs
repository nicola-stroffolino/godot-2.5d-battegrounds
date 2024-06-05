using Godot;
using System;

public partial class Player : GameEntity3D {
    public override void _Process(double delta) {
        var inputDirection = new Vector2 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		};
    }

    public override bool WantsToMove() =>
		Input.IsActionPressed("move_up") ||
		Input.IsActionPressed("move_down") ||
		Input.IsActionPressed("move_right") ||
		Input.IsActionPressed("move_left");

	public override bool WantsToAttack() =>
		Input.IsActionPressed("attack");
}
