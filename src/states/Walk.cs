using Godot;
using System;

public partial class Walk : State {
    public Vector2 SpriteDirection { get; set; } // should NEVER be equal to Vector2.Zero

    public override string StatePhysicsProcess(float delta) {
        var inputDirection = new Vector2 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		};

        if (inputDirection == Vector2.Zero) return "Idle";
        SpriteDirection = inputDirection;

        Sprite.Play("walk_" + Game.Directions[inputDirection]);

        inputDirection = inputDirection.Normalized();
        Player.Velocity = new(inputDirection.X * Player.Speed, 0f, inputDirection.Y * Player.Speed);

        return base.StatePhysicsProcess(delta);
    }

    public override string StateInput(InputEvent @event) {
        if (@event.IsActionPressed("attack")) return "Attack";

        return base.StateInput(@event);
    }
}
