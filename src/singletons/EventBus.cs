using Godot;

public partial class EventBus : Node {
    [Signal] public delegate void StateTransitionedEventHandler(State fromState, State toState);
    public static EventBus Instance { get; set; }

    public override void _Ready() {
        Instance = this;
    }
}