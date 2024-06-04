using Godot;
using System;

public partial class Idle : State {
    public override void Enter() {
        Sprite.Play("idle_" + Game.Directions[Player.SpriteDirection]);

        Player.Velocity = Vector3.Zero;
    }

    public override string StatePhysicsProcess(float delta) {
        
        return base.StateProcess(delta);
    }

    public override string StateInput(InputEvent @event) {
        if (@event.IsActionPressed("move_up") ||
        @event.IsActionPressed("move_down") ||
        @event.IsActionPressed("move_right") ||
        @event.IsActionPressed("move_left")) return "Walk";

        if (@event.IsActionPressed("attack")) return "Attack";

        return base.StateInput(@event);
    }
}
