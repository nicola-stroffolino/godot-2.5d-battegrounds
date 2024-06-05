using Godot;
using System;

public partial class Idle : State {
    public override void Enter() {
        Sprite.Play("idle_" + Game.Directions[StateOwner.SpriteDirection]);

        StateOwner.Velocity = Vector3.Zero;
    }

    public override string StatePhysicsProcess(float delta) {
        if (StateOwner.WantsToMove()) return "Walk";
        
        if (StateOwner.WantsToAttack()) return "Attack";

        return base.StateProcess(delta);
    }
}
