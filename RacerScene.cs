using Godot;
using System;

public partial class RacerScene : Node3D
{
	private CamRig cam;
	float longitudeDeg;
	float latitudeDeg;
	float camDist;
	Vector3 camTg;       // coords of camera target

	Cart cart;           // model of the cart
	double wheelRad;     // wheel radius
	double wheelRadS;    // wheel radius of steared wheel
	double wheelSep;     // distance separating the two wheels.
	double baseLen;      // base length, from rear wheel axle to steer axis.
	double casterLen;    // length of caster
	double cgDist;       // longit distance from wheel axle to CG
	float tireThk;       // tire thickness
	float wheelThk;      // wheel thickness
	float tireThkS;      // thickness of steered wheel.
	float frameThk;      // frame thickness

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("RacerScene");
		longitudeDeg = 30.0f;
		latitudeDeg = 20.0f;
		camDist = 4.0f;

		camTg = new Vector3(0.0f, 1.0f, 0.0f);
		cam = GetNode<CamRig>("CamRig");
		cam.LongitudeDeg = longitudeDeg;
		cam.LatitudeDeg = latitudeDeg;
		cam.Distance = camDist;
		cam.Target = camTg;
		//cam.FOVDeg = camFOV;

		cart = GetNode<Cart>("Cart");
		wheelRad = 0.75;
		wheelRadS = 0.2;
		wheelSep = 1.0;
		baseLen = 1.3;
		casterLen = 0.3;
		cgDist = 0.6;
		tireThk = 0.08f;
		wheelThk = 0.5f * tireThk;
		tireThkS = 0.2f * (float)wheelRadS;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
