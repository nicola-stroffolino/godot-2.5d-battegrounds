using Godot;
using System;

public partial class Attack : State {
    public HitBox AttackHitBox { get; set; }
    public Timer Timer { get; set; }

    public override void _Ready() {
        Timer = GetNode<Timer>("Timer");
    }

    public override void Enter() {        
        StateOwner.Velocity = Vector3.Zero;

        AttackHitBox.AttackVelocity = new Vector3() { 
            X = StateOwner.SpriteDirection.X * 10, 
            Z = StateOwner.SpriteDirection.Y * 10 
        };
        // AttackHitBox.Monitorable = true;

        // Sprite.Play("attack_" + Game.Directions[StateOwner.SpriteDirection]);
        Timer.Start();
        Anim.Play("fighting/right_punch");
    }

    public override void Exit() {
        // AttackHitBox.Monitorable = false;
    }

    public override string StateProcess(float delta) {
        if (Timer.TimeLeft != 0) return null;

        if (StateOwner.WantsToMove()) return "Walk";
        else return "Idle";
    }
}
