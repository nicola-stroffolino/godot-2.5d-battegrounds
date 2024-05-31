using Godot;
using System;

public partial class Walk : State {
    public override string StatePhysicsProcess(float delta) {
        var inputDirection = new Vector2 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		};

        if (inputDirection == Vector2.Zero) return "Idle";

        Sprite.Play("walk_" + Game.Directions[inputDirection]);

        return base.StatePhysicsProcess(delta);
    }
}
