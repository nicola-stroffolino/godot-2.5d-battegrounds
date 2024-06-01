using Godot;
using System;

public partial class Attack : State {
    public Area3D AttackHitBox { get; set; }
    public Timer Timer { get; set; }
    private bool _exited = false;

    public override void _Ready() {
        Timer = GetNode<Timer>("Timer");
        Timer.Connect(Timer.SignalName.Timeout, Callable.From(SetExited));
    }

    public override void Enter() {        
        Player.Velocity = Vector3.Zero;
        AttackHitBox.Monitoring = true;
        Timer.Start();
    }

    public override void Exit() {
        AttackHitBox.Monitoring = false;
        _exited = false;
    }

    public override string StateProcess(float delta) {
        if (_exited) return "Idle";
        return base.StateProcess(delta);
    }

    public void SetExited() {
        _exited = true;
    }
}
