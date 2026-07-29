using Godot;
using System;

namespace DoubleCorvid.DungeonCrawlExtraction.PC;

public partial class PlayerCharacterContoller : CharacterBody3D {
	[Export]
	public float MoveSpeed { get; private set; } = 8f;

	[Export]
	public float Accelation { get; private set; } = 20f;

	[Export]
	public float RotationSpeed { get; private set; } = 20f;

	[Export]
	public float JumpImpulse { get; private set; } = 20f;

	[Export]
	public float CameraTiltUpperLimit { get; private set; } = 20f;

	[Export]
	public float CameraTiltLowerLimit { get; private set; } = 20f;

	[Export]
	public CameraController CameraController { get; private set; }

	private Vector2 _cameraInputDirection = Vector2.Zero;

	private Vector3 _lastMovementDirection = Vector3.Back;

	private float _gravity = -30f;

    public override void _PhysicsProcess(double delta) {
    }
}
