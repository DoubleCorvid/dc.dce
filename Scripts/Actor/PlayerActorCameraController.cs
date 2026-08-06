using System;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Actor;

public partial class PlayerActorCameraController : ActorCameraController {
	[Export]
	public SpringArm3D? SpringArm { get; private set; }

	[Export]
	public bool IsFPP { get; private set; } = true;

	[Export]
	public float SpringArmLengthTPP { get; private set; } = 10;

    public override void _Ready() {
        base._Ready();
    }

    public override void _Input (InputEvent @event) {
		if (SpringArm is null) {
			throw new NullReferenceException ();
		}
		
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
			NextLookAngle = mouseMotion.ScreenRelative * LookSensitivity;
		}
    }
}
