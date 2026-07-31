using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.PC;

public partial class CameraController : Node3D {
	[Export]
	public float MouseSensitvity { get; private set; } = 1f;

	[Export]
	public float CameraTiltUpperLimit { get; private set; } = Mathf.Pi / 3;

	[Export]
	public float CameraTiltLowerLimit { get; private set; } = -Mathf.Pi / 6;

	[Export]
	public SpringArm3D SpringArm { get; private set; }

	[Export]
	public bool IsFPP { get; private set; } = true;

	[Export]
	public float SpringArmLengthTPP { get; private set; } = 10;

	[Export]
	public Camera3D Camera { get; private set; }

	private Vector2 _cameraInputDirection = Vector2.Zero;

    public override void _Input (InputEvent @event) {
		if (@event.IsActionPressed ("ui_cancel")) {
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if (@event.IsActionPressed ("left_click")) {
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}

		if (@event.IsActionPressed ("perspective_switch")) {
			if (IsFPP) {
				IsFPP = false;
				SpringArm.SpringLength = SpringArmLengthTPP;
			}
			else {
				IsFPP = true;
				SpringArm.SpringLength = 0;
			}
		}
    }

    public override void _UnhandledInput (InputEvent @event) {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
			_cameraInputDirection = mouseMotion.ScreenRelative * MouseSensitvity;
		}
    }

    public override void _PhysicsProcess (double delta) {
		var deltaF = (float) delta;

		var cameraRotation = Rotation;

		var tiltTarget = Rotation.X + -_cameraInputDirection.Y * deltaF;

        var newCameraRotationX = Mathf.Clamp (tiltTarget, CameraTiltLowerLimit, CameraTiltUpperLimit);
		var cameraRotationYDelta = _cameraInputDirection.X * deltaF;

		Rotation = new Vector3 (newCameraRotationX, cameraRotation.Y - cameraRotationYDelta, cameraRotation.Z);

		_cameraInputDirection = Vector2.Zero;
    }
}
