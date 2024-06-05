using Godot;
using System;
using System.Linq;

public partial class EnemyWalk : State {
    public override string StatePhysicsProcess(float delta) {
		Vector3 d = Enemy.GlobalPosition.DirectionTo(Enemy.Target.GlobalPosition);

		var angle = Mathf.Atan2(d.Z, d.X);
		angle = Mathf.Snapped(angle, Mathf.Pi / 4) / (Mathf.Pi / 4);

		int angleIndex = Mathf.Wrap((int)angle, 0, 8);
		Enemy.SpriteDirection = Game.DirectionFromIndex(angleIndex);

		var rotation = Mathf.Atan2(Enemy.SpriteDirection.X, Enemy.SpriteDirection.Y);
        Enemy.Rotation = new(0, rotation, 0);

		Sprite.Play("walk_" + Game.Directions[Enemy.SpriteDirection]);

		var direction = d.Normalized();
		Enemy.SetVelocity(new(direction.X * Enemy.Speed, 0f, direction.Z * Enemy.Speed));
		// Enemy.Velocity = new(direction.X * Enemy.Speed, 0f, direction.Z * Enemy.Speed);

        return base.StatePhysicsProcess(delta);
    }
}