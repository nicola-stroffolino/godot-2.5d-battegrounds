using Godot;

public partial class Signals : Node {
    [Signal] public delegate void StateTransitionedEventHandler(State fromState, State toState);
}