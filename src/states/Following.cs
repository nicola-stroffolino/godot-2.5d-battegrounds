using Godot;
using System;
using System.Linq;

public partial class Following : State {
    public override string StateProcess(float delta) {
		// Vector3 d = StateOwner.GlobalPosition.DirectionTo(StateOwner.Target.GlobalPosition);

		// var angle = Mathf.Atan2(d.Z, d.X);
		// angle = Mathf.Snapped(angle, Mathf.Pi / 4) / (Mathf.Pi / 4);

		// int angleIndex = Mathf.Wrap((int)angle, 0, 8);
		// StateOwner.SpriteDirection = Game.DirectionFromIndex(angleIndex);

		// var rotation = Mathf.Atan2(StateOwner.SpriteDirection.X, StateOwner.SpriteDirection.Y);
        // StateOwner.Rotation = new(0, rotation, 0);

		// Sprite.Play("walk_" + Game.Directions[StateOwner.SpriteDirection]);

		// var direction = d.Normalized();
		// StateOwner.SetVelocity(new(direction.X * Enemy.Speed, 0f, direction.Z * Enemy.Speed));
		// // Enemy.Velocity = new(direction.X * Enemy.Speed, 0f, direction.Z * Enemy.Speed);

        return base.StatePhysicsProcess(delta);
    }
}