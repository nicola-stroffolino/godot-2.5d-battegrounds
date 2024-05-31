using Godot;
using System;

public partial class Attack : State {
    public Area3D AttackHitBox { get; set; }

    public override void Enter() {
        Player.Velocity = Vector3.Zero;
        AttackHitBox.Monitoring = true;
    }

    public override void Exit() {
        AttackHitBox.Monitoring = false;
    }

    public void OnAttackHitBoxEntered(Node3D body) {
        if (body is Block b) {
            b.Destroy();
        }
    }
}
