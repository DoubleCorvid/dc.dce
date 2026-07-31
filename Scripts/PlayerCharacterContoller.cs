using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction;

public partial class PlayerCharacterContoller : CharacterBody3D {
	[Export]
	public float WalkSpeed { get; private set; } = 8f;

	[Export]
	public float RunSpeed { get; private set; } = 16f;

	[Export]
	public float Accelation { get; private set; } = 100f;

	[Export]
	public float RotationSpeed { get; private set; } = 20f;

	[Export]
	public float JumpImpulse { get; private set; } = 9.806f;

	[Export]
	public float Gravity = -9.806f;

	[Export]
	public float CameraTiltUpperLimit { get; private set; } = 20f;

	[Export]
	public float CameraTiltLowerLimit { get; private set; } = 20f;

	[Export]
	public CameraController CameraController { get; private set; }

	private Vector2 _cameraInputDirection = Vector2.Zero;

	private Vector3 _lastMovementDirection = Vector3.Back;

	private bool _running = false;

    public override void _UnhandledInput (InputEvent @event) {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
			_cameraInputDirection = mouseMotion.ScreenRelative * CameraController.MouseSensitvity;
		}

        if (@event.IsAction ("run") && !_running) {
			_running = true;
		}

		if (@event.IsActionReleased ("run") && _running) {
			_running = false;
		}
    }

    public override void _PhysicsProcess (double delta) {
		var bodyRotationY = _cameraInputDirection.X * (float) delta;

		_cameraInputDirection = Vector2.Zero;

		Rotate (Vector3.Up, -bodyRotationY);

		var rawInput = Input.GetVector ("move_left", "move_right", "move_forward", "move_backward");
		var forward = CameraController.Camera.GlobalBasis.Z;
		var right = CameraController.Camera.GlobalBasis.X;

		var moveDirection = forward * rawInput.Y + right * rawInput.X;

		moveDirection.Y = 0;

		moveDirection = moveDirection.Normalized ();

		var moveSpeed = (_running ? RunSpeed : WalkSpeed);

		Velocity = new Vector3 (Velocity.X, 0, Velocity.Z);

		Velocity = Velocity.MoveToward (moveDirection * moveSpeed, Accelation * (float) delta);

		Velocity += new Vector3 (0, Velocity.Y + Gravity * (float) delta, 0);

		var startedJumping = Input.IsActionJustPressed ("jump") && IsOnFloor ();

		if (startedJumping) {
			Velocity += new Vector3 (0, JumpImpulse, 0);
		}

		MoveAndSlide ();
    }
}
