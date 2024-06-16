using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class State : Node {
	public FSM StateMachine { get; set; }
	public GameEntity3D StateOwner { get; set; }
	public AnimationPlayer Anim { get; set; }
	// public AnimatedSprite3D Sprite { get; set; }

	public virtual void Enter() {}
	public virtual void Exit() {}
	public virtual string StateProcess(float delta) => null;
	public virtual string StatePhysicsProcess(float delta) => null;
	public virtual string StateInput(InputEvent @event) => null;
	public virtual string StateUnhandledInput(InputEvent @event) => null;
	public virtual string StateUnhandledKeyInput(InputEvent @event) => null;
}