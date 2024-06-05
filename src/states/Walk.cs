using Godot;
using System;

public partial class Walk : State {
    public override string StateProcess(float delta) {
        if (StateOwner.WantsToAttack()) return "Attack";
    
        if (!StateOwner.WantsToMove()) return "Idle";
    
        return base.StateProcess(delta);
    }

    public override string StatePhysicsProcess(float delta) {
        StateOwner.SpriteDirection = StateOwner.MoveDirection;

        var rotation = Mathf.Atan2(StateOwner.SpriteDirection.X, StateOwner.SpriteDirection.Y);
        StateOwner.Rotation = new(0, rotation, 0);

        Sprite.Play("walk_" + Game.Directions[StateOwner.SpriteDirection]);

        var direction = StateOwner.SpriteDirection.Normalized();
        StateOwner.Velocity = new(direction.X * GameEntity3D.Speed, 0f, direction.Y * GameEntity3D.Speed);

        return base.StatePhysicsProcess(delta);
    }

    public override string StateInput(InputEvent @event) {

        return base.StateInput(@event);
    }
}
