using Godot;
using System;

public partial class Attack : State {
    public HitBox AttackHitBox { get; set; }
    public Timer Timer { get; set; }
    private bool _exited = false;

    public override void _Ready() {
        Timer = GetNode<Timer>("Timer");
        Timer.Connect(Timer.SignalName.Timeout, Callable.From(SetExited));
    }

    public override void Enter() {        
        StateOwner.Velocity = Vector3.Zero;

        AttackHitBox.AttackVelocity = new Vector3(StateOwner.SpriteDirection.X, 0, StateOwner.SpriteDirection.Y);
        AttackHitBox.Monitorable = true;
        
        Sprite.Play("attack_" + Game.Directions[StateOwner.SpriteDirection]);
        Timer.Start();
    }

    public override void Exit() {
        AttackHitBox.Monitorable = false;
        _exited = false;
    }

    public override string StateProcess(float delta) {
        if (!_exited) return null;

        if (StateOwner.WantsToMove()) return "Walk";

        return "Idle";
    }

    public void SetExited() {
        _exited = true;
    }
}
