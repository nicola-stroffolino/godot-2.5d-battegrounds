using Godot;
using System;

public partial class Idle : State {
    public override void Enter() {
        var prevState = StateMachine.PreviousState;
        if (prevState is Walk ws) {
            Sprite.Play("idle_" + Game.Directions[ws.SpriteDirection]);
        } else {
            Sprite.Play("idle_" + Game.Directions[Vector2.Down]);
        }

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
