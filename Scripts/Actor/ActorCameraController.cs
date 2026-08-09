using System;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Actor;

public partial class ActorCameraController : Node3D {
	[ExportGroup	 ("Camera Settings")]
	[Export]
	public float LookSensitivity { get; set; } = 1f;

	[Export]
	public float CameraTiltUpperLimit { get; set; } = Mathf.Pi / 2 - 0.00001f;

	[Export]
	public float CameraTiltLowerLimit { get; set; } = -Mathf.Pi / 3;

	[ExportCategory ("Children")]
	[Export]
	public Camera3D? Camera { get; private set; }

	[Export]
	public RayCast3D? LookCast { get; private set; }

	public Vector2 NextLookAngle { get; set; } = Vector2.Zero;

    public override void _PhysicsProcess (double delta) {
		if (Camera is null) {
			throw new NullReferenceException ("Camera must be set in the editor");
		}

		Rotate (Vector3.Right, -NextLookAngle.Y * (float) delta);

		Rotation = new Vector3 (Mathf.Clamp (Rotation.X, CameraTiltLowerLimit, CameraTiltUpperLimit), Rotation.Y, Rotation.Z);

		NextLookAngle = Vector2.Zero;
    }
}