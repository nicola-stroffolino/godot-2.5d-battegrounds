using Godot;
using System;

public partial class EnemyIdle : State {
	public Enemy Enemy { get; set; }

    public override void Enter() {
        Sprite.Play("idle_" + Game.Directions[Enemy.SpriteDirection]);

        Enemy.Velocity = Vector3.Zero;
    }

    public override string StatePhysicsProcess(float delta) {
		if (Enemy.Target is not null) return "EnemyWalk";

        return base.StateProcess(delta);
    }
}
