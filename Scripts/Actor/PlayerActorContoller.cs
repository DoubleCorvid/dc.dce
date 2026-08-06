using System;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Actor;

public partial class PlayerActorContoller : ActorContoller {
    public override void _UnhandledInput (InputEvent @event) {
		if (CameraController is null) {
			throw new NullReferenceException ();
		}

        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
			NextRotateDirection = mouseMotion.ScreenRelative * CameraController.LookSensitivity;
		}

        if (@event.IsAction ("run") && !Running) {
			Running = true;
		}

		if (@event.IsActionReleased ("run") && Running) {
			Running = false;
		}
    }

    public override void _PhysicsProcess (double delta) {
		if (CameraController?.Camera is null) {
			throw new NullReferenceException ();
		}

		NextMoveDistance = Input.GetVector ("move_left", "move_right", "move_forward", "move_backward");
		Forward = CameraController.Camera.GlobalBasis.Z;
		Right = CameraController.Camera.GlobalBasis.X;

		JustJumped = Input.IsActionJustPressed ("jump");

		base._PhysicsProcess (delta);
    }
}
