using Godot;
using System;

public partial class Idle : State {
    public override string StateInput(InputEvent @event) {
        if (@event.IsActionPressed("move_up") ||
        @event.IsActionPressed("move_down") ||
        @event.IsActionPressed("move_right") ||
        @event.IsActionPressed("move_left")) return "Walk";

        return base.StateInput(@event);
    }
}
