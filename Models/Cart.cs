using Godot;
using System;

public partial class Cart : Node3D
{
	MeshInstance3D axleBar;
	BoxMesh axleBarMesh;

	float barWidth;        // width of bar
	float axleBarLen;      // length of the axle bar
	float longBarLen;      // length of the longitudinal bar.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Cart");

		// bar dimensions
		barWidth = 0.08f;
		axleBarLen = 1.0f;
		longBarLen = 1.3f;

		axleBar = GetNode<MeshInstance3D>("CartFrame/AxleBar");
		axleBarMesh = (BoxMesh)axleBar.Mesh;
		axleBarMesh.Size = new Vector3(barWidth, barWidth, axleBarLen);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
