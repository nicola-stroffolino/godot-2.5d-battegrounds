using Godot;
using System;

public partial class Block : CsgBox3D {
	public AnimationPlayer AnimPlayer { get; set; }
	public GpuParticles3D Particles { get; set; }
	public Area3D HurtBox { get; set; }

	public override void _Ready() {
		AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		Particles = GetNode<GpuParticles3D>("GPUParticles3D");
		HurtBox = GetNode<Area3D>("HurtBox");
		HurtBox.Connect(Area3D.SignalName.AreaEntered, new Callable(this, MethodName.Destroy));
	}

	public void Destroy(HitBox hitBox) {
		var particleMaterial = (ParticleProcessMaterial)Particles.ProcessMaterial;
		particleMaterial.Direction = hitBox.AttackVelocity;

		AnimPlayer.Play("break");
	}
}
