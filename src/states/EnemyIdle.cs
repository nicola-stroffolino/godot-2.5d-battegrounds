using Godot;
using System;

public partial class EnemyIdle : State {
    public override void Enter() {
        Sprite.Play("idle_" + Game.Directions[StateOwner.SpriteDirection]);

        StateOwner.Velocity = Vector3.Zero;
    }

    public override string StatePhysicsProcess(float delta) {
		if ((StateOwner as Enemy).Target is not null) return "EnemyWalk";

        return base.StateProcess(delta);
    }
}
