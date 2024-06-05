using Godot;
using System;

public partial class HurtBox : Area3D {
    public Node3D Parent { get; set; }

    public override void _Ready() {
        Connect(SignalName.AreaEntered, new Callable(this, MethodName.OnAreaEntered));
        Parent = (Node3D)GetParent();
    }

    public void OnAreaEntered(HitBox hitBox) {
        if (hitBox.GetParent() == Parent) return;

        if (Parent is not Enemy e) return;

        // e.Velocity += hitBox.AttackVelocity * 100;
        e.SetVelocity(e.GetVelocity() + hitBox.AttackVelocity * 100);
        GD.Print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    }
}
