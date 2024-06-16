using Godot;
using System;

public partial class Wandering : State {
    // public Timer WanderTimer { get; set; }
    // private float _changeDirectionTimer = 0.0f;
    // private float _changeDirectionInterval = 2.0f;
    
    public override void _Ready() {
        // WanderTimer = GetNode<Timer>("WanderTimer");
        // WanderTimer.Connect(Timer.SignalName.Timeout, Callable.From(MoveABit));
    }
 
    public override void Enter() {
        // WanderTimer.Start();
    }

    public override void Exit() {
        // WanderTimer.Stop();
    }

    public override string StateProcess(float delta) {
        
        // if(StateOwner.MoveDirection != Vector2.Zero) {
        //     _changeDirectionTimer += delta;

        //     if (_changeDirectionTimer >= _changeDirectionInterval) {
        //         StateOwner.MoveDirection = Vector2.Zero;
        //         _changeDirectionTimer = 0.0f;
        //     }
        // }

        return base.StateProcess(delta);
    }

    public void MoveABit() {
        // StateOwner.MoveDirection = Game.GetRandomDirection();
    }
}
