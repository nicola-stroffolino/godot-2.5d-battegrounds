using Godot;
using System;

public partial class Idle : State {
    public override void Enter() {
        // Sprite.Play("idle_" + Game.Directions[StateOwner.SpriteDirection]);
        Anim.Play("movement/idle");

        // StateOwner.Velocity = Vector3.Zero;
    }

    public override string StateProcess(float delta) {
        StateOwner.Velocity = new Vector3() {
            X = Mathf.Lerp(StateOwner.Velocity.X, 0f, delta * 20),
            Z = Mathf.Lerp(StateOwner.Velocity.Z, 0f, delta * 20),
        };

        if (StateOwner.WantsToAttack()) return "Attack";
        
        if (StateOwner.MoveDirection != Vector2.Zero) return "Walk";

        return base.StateProcess(delta);
    }
}
