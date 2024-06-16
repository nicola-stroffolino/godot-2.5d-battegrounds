using Godot;
using System;

public partial class Walk : State {
    public override string StateProcess(float delta) {
        if (StateOwner.WantsToAttack()) return "Attack";
    
        if (StateOwner.MoveDirection == Vector2.Zero) return "Idle";

        // GD.Print(StateOwner.MoveDirection);

        StateOwner.SpriteDirection = StateOwner.MoveDirection;

        var rotation = Mathf.Atan2(StateOwner.SpriteDirection.X, StateOwner.SpriteDirection.Y);
        StateOwner.Rotation = new(0, rotation, 0);

        // Sprite.Play("walk_" + Game.Directions[StateOwner.SpriteDirection]);
        Anim.Play("movement/walk");

        StateOwner.MoveDirection = StateOwner.MoveDirection.Normalized();
        StateOwner.Velocity = new() {
            X = StateOwner.MoveDirection.X * StateOwner.Speed,
            Z = StateOwner.MoveDirection.Y * StateOwner.Speed
        };
        
        return base.StateProcess(delta);
    }
}
