using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.PC;

public partial class PlayerCharacterContoller : CharacterBody3D {
	[Export]
	public float MoveSpeed { get; private set; } = 8f;

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

	private bool _sprinting = false;

    public override void _Input (InputEvent @event) {
        base._Input(@event);
    }

    public override void _UnhandledInput (InputEvent @event) {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
			_cameraInputDirection = mouseMotion.ScreenRelative * CameraController.MouseSensitvity;
		}
    }

    public override void _PhysicsProcess(double delta) {
		var rawInput = Input.GetVector ("move_left", "move_right", "move_forward", "move_backward");
		var forward = CameraController.Camera.GlobalBasis.Z;
		var right = CameraController.Camera.GlobalBasis.X;

		var moveDirection = forward * rawInput.Y + right * rawInput.X;

		moveDirection.Y = 0;

		moveDirection = moveDirection.Normalized ();

		var yVelocity = Velocity.Y;

		Velocity = new Vector3 (Velocity.X, 0, Velocity.Z);

		Velocity = Velocity.MoveToward (moveDirection * MoveSpeed, Accelation * (float) delta);

		Velocity = new Vector3 (Velocity.X, yVelocity + Gravity * (float) delta, Velocity.Z);

		var startedJumping = Input.IsActionJustPressed ("jump") && IsOnFloor ();

		if (startedJumping) {
			Velocity += new Vector3 (0, JumpImpulse, 0);
		}

		MoveAndSlide ();
    }
}
