using System.Diagnostics;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Actor;

public partial class ActorContoller : CharacterBody3D {
	[Export]
	public float WalkSpeed { get; private set; } = 8f;

	[Export]
	public float RunSpeed { get; private set; } = 16f;

	[Export]
	public float Accelation { get; private set; } = 100f;

	[Export]
	public float RotationSpeed { get; private set; } = 20f;

	[Export]
	public float JumpImpulse { get; private set; } = 4f;

	[Export]
	public float Gravity = -9.806f;

	[Export]
	public ActorCameraController? CameraController { get; private set; }

	[Export]
	public Vector3 Forward { get; set; } = Vector3.Forward;

	[Export]
	public Vector3 Right { get; set; } = Vector3.Right;

	public Vector2 NextMoveDistance { get; set; } = Vector2.Zero;

	public Vector2 NextRotateDirection { get; set; } = Vector2.Zero;

	public bool Running { get; set; } = false;

	public bool JustJumped { get; set; } = false;

    public override void _PhysicsProcess (double delta) {
		var bodyRotationY = NextRotateDirection.X * (float) delta;

		NextRotateDirection = Vector2.Zero;

		Rotate (Vector3.Up, -bodyRotationY);

		var moveDirection = Forward * NextMoveDistance.Y + Right * NextMoveDistance.X;

		NextMoveDistance = Vector2.Zero;

		moveDirection.Y = 0;

		moveDirection = moveDirection.Normalized ();

		var moveSpeed = (Running ? RunSpeed : WalkSpeed);

		var yVelocity = Velocity.Y;

		if (JustJumped && IsOnFloor ()) {
			yVelocity += JumpImpulse;

			JustJumped = false;
		}

		var moveTarget = new Vector3 (Velocity.X, 0, Velocity.Z).MoveToward (moveDirection * moveSpeed, Accelation * (float) delta) + new Vector3 (0, yVelocity + Gravity * (float) delta, 0);

		Velocity = moveTarget;

		MoveAndSlide ();
    }
}