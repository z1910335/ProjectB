//============================================================================
// RacerScene
//============================================================================
using Godot;
using System;
using System.Linq.Expressions;

public partial class RacerScene : Node3D
{
	private CamRig cam;
	float longitudeDeg;
	float latitudeDeg;
	float camDist;
	float camFOV;
	Vector3 camTg;       // coords of camera target

	Cart cart;           // model of the cart
	double wheelRad;     // wheel radius
	double wheelRadS;    // wheel radius of steared wheel
	double wheelSep;     // distance separating the two wheels.
	double baseLen;      // base length, from rear wheel axle to steer axis.
	double casterLen;    // length of caster
	double cgDist;       // longit distance from wheel axle to CG
	float tireWd;        // tire width
	float wheelWd;      // wheel width
	float tireWdS;      // thickness of steered wheel
	float wheelWdS;     // width of steered wheel
	float frameThk;      // frame thickness
	double m;            // mass of racer
	double rGyr;         // radius of gyration

	double steerSig;     // steer signal from pilot

	RollerRacer racer;   // simulation of the Roller Racer

	//------------------------------------------------------------------------
	// _Ready: Called when the node enters the scene tree for the first time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
		longitudeDeg = 30.0f;
		latitudeDeg = 20.0f;
		camDist = 4.0f;
		camFOV = 55.0f;

		camTg = new Vector3(0.0f, 1.0f, 0.0f);
		cam = GetNode<CamRig>("CamRig");
		cam.LongitudeDeg = longitudeDeg;
		cam.LatitudeDeg = latitudeDeg;
		cam.Distance = camDist;
		cam.Target = camTg;
		cam.FOVDeg = camFOV;

		cart = GetNode<Cart>("Cart");
		wheelRad = 0.5 * 0.75;  cart.WheelRadius = (float)wheelRad;
		wheelRadS = 0.15;       cart.SteeredWheelRadius = (float)wheelRadS;
		wheelSep = 1.0;         cart.WheelSeparation = (float)wheelSep;
		baseLen = 1.3;			cart.BaseLength = (float)baseLen;
		casterLen = 0.3; 		cart.CasterLength = (float)casterLen;
		cgDist = 0.6;
		tireWd = 0.08f;         cart.TireWidth = tireWd;
		wheelWd = 0.5f * tireWd; 	cart.WheelWidth = wheelWd;
		tireWdS = 0.05f;		cart.SteeredTireWidth = tireWdS;
		wheelWdS = 0.7f * tireWdS;	cart.SteeredWheelWidth = wheelWdS;
		frameThk = 0.08f;		cart.FrameBarWidth = frameThk;
		cart.BuildModel();

		m = 25.0;
		rGyr = 0.3;
		racer = new RollerRacer();    // simulation
	}

	//------------------------------------------------------------------------
	// _Process
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//------------------------------------------------------------------------
	public override void _Process(double delta)
	{
		ProcessPilotInput();

		float st = -50.0f * (float)steerSig;
		cart.SteerAngle = Mathf.DegToRad(st);
	}

	//------------------------------------------------------------------------
	// ProcessPilotInput:
	//------------------------------------------------------------------------
	private void ProcessPilotInput()
	{
		//deltaSig = (double)(Input.GetActionStrength("ui_left") -
		//	Input.GetActionStrength("ui_right"));

		steerSig = (double)Input.GetJoyAxis(0, JoyAxis.LeftX);
		if(Math.Abs(steerSig) < 0.01)
			steerSig = 0.0;

		//GD.Print(steerSig);
	}

}
