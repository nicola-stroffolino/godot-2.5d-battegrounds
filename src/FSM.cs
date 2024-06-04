using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Reflection;

[GlobalClass]
public partial class FSM : Node {
	[Export] public State InitialState { get; set; }
	public State PreviousState { get; set; }
	public State CurrentState { get; set; }
	public Dictionary<string, State> States { get; set; }

	public override void _Ready() {
		States = new();
        foreach (var child in GetChildren()) {
        	if (child is State s) {
				s.StateMachine = this;
				
				if (s is EnemyIdle ei) ei.Enemy = (Enemy)GetParent();
				else if (s is EnemyWalk ew) ew.Enemy = (Enemy)GetParent();
				else s.Player = (Player)GetParent();
				
				s.Sprite = GetParent().GetNode<AnimatedSprite3D>("%Sprite");
				if (s is Attack a) a.AttackHitBox = GetParent().GetNode<HitBox>("HitBox");
				States[s.Name.ToString().ToLower()] = s;
        	}
        }
		ChangeState(InitialState);
	}
	
	public override void _Process(double delta) {
		var newState = CurrentState.StateProcess((float)delta);
		if (newState is not null) ChangeState(newState);
	}

	public override void _PhysicsProcess(double delta) {
		var newState = CurrentState.StatePhysicsProcess((float)delta);
		if (newState is not null) ChangeState(newState);
	}

	public override void _Input(InputEvent @event) {
		var newState = CurrentState.StateInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public override void _UnhandledInput(InputEvent @event) {
		var newState = CurrentState.StateUnhandledInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public override void _UnhandledKeyInput(InputEvent @event) {
		var newState = CurrentState.StateUnhandledKeyInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public void ChangeState(State newState) {
		CurrentState?.Exit();
		PreviousState = CurrentState;
		CurrentState = newState;
		CurrentState.Enter();

		EventBus.Instance.EmitSignal(EventBus.SignalName.StateTransitioned, PreviousState, CurrentState);
	}

	public void ChangeState(string newStateName) {
		var newState = States[newStateName.ToLower()];

		CurrentState?.Exit();
		PreviousState = CurrentState;
		CurrentState = newState;
		CurrentState.Enter();

		EventBus.Instance.EmitSignal(EventBus.SignalName.StateTransitioned, PreviousState, CurrentState);
	}

	public State GetState<T>() where T: State => States.OfType<T>().FirstOrDefault();
}
