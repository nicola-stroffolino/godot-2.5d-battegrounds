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
        Player.Velocity = Vector3.Zero;

        AttackHitBox.AttackVelocity = new Vector3(Player.SpriteDirection.X, 0, Player.SpriteDirection.Y);
        AttackHitBox.Monitorable = true;
        
        Sprite.Play("attack_" + Game.Directions[Player.SpriteDirection]);
        Timer.Start();
    }

    public override void Exit() {
        AttackHitBox.Monitorable = false;
        _exited = false;
    }

    public override string StateProcess(float delta) {
        if (!_exited) return null;

        if (Input.IsActionPressed("move_up") ||
        Input.IsActionPressed("move_down") ||
        Input.IsActionPressed("move_right") ||
        Input.IsActionPressed("move_left")) return "Walk";

        return "Idle";
    }

    public void SetExited() {
        _exited = true;
    }
}
