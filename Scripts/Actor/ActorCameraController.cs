using System.Diagnostics.CodeAnalysis;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Actor;

public partial class ActorCameraController : Node3D {
	[Export]
	public float LookSensitivity { get; set; } = 1f;

	[Export]
	public float CameraTiltUpperLimit { get; set; } = Mathf.Pi / 2 - 0.00001f;

	[Export]
	public float CameraTiltLowerLimit { get; set; } = -Mathf.Pi / 3;

	[Export]
	public Camera3D? Camera { get; private set; }

	public Vector2 NextLookAngle { get; set; } = Vector2.Zero;

    public override void _PhysicsProcess (double delta) {
		Rotate (Vector3.Right, -NextLookAngle.Y * (float) delta);

		Rotation = new Vector3 (Mathf.Clamp (Rotation.X, CameraTiltLowerLimit, CameraTiltUpperLimit), Rotation.Y, Rotation.Z);

		NextLookAngle = Vector2.Zero;
    }
}