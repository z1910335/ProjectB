using Godot;
using System;

public partial class Cart : Node3D
{
	//Node3D cartFrame;
	// MeshInstance3D axleBar;
	// BoxMesh axleBarMesh;
	Node3D wheelFrameL;
	Node3D wheelFrameR;

	float wheelRad;        // rear wheel radius
	float wheelRadS;       // radius of steered wheel
	float wheelSep;        // separation of rear wheels, center to center
	float baseLen;         // base length rear axle to steer axis
	float casterLen;       // caster length for front wheel
	float tireWd;         // rear tire width
	float wheelWd;        // rear wheel width
	float tireWdS;        // steered tire width
	float barWidth;        // width of bar
	// float axleBarLen;      // length of the axle bar
	// float longBarLen;      // length of the longitudinal bar.
	float cgDist;          // dist of cg ahead of wheel axle

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// default parameters
		wheelRad = 0.5f * 0.75f;
		wheelRadS = 0.2f;
		wheelSep = 1.0f;
		baseLen = 1.3f;
		casterLen = 0.3f;
		tireWd = 0.08f;
		wheelWd = 0.5f * tireWd;
		tireWdS = 0.2f * (float)wheelRadS;
		barWidth = 0.08f;
		cgDist = 0.6f;

		// axleBarLen = 1.0f;
		// longBarLen = 1.3f;

		//axleBar = GetNode<MeshInstance3D>("CartFrame/AxleBar");
		//cartFrame = GetNode<Node3D>("CartFrame");
		wheelFrameL = GetNode<Node3D>("CartFrame/WheelFrameL");
		wheelFrameR = GetNode<Node3D>("CartFrame/WheelFrameR");
		BuildModel();
		// axleBarMesh = (BoxMesh)axleBar.Mesh;
		// axleBarMesh.Size = new Vector3(barWidth, barWidth, axleBarLen);
	}

	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}

	//------------------------------------------------------------------------
	// BuildModel:
	//------------------------------------------------------------------------
	public void BuildModel()
	{
		// axle bar
		MeshInstance3D axleBar = GetNode<MeshInstance3D>("CartFrame/AxleBar");
		BoxMesh axleBarMesh = (BoxMesh)axleBar.Mesh;
		float axleBarLen = wheelSep - 1.5f*wheelWd;
		axleBarMesh.Size = new Vector3(barWidth, barWidth, axleBarLen);

		// longitudinal bar
		MeshInstance3D longBar = GetNode<MeshInstance3D>("CartFrame/LongBar");
		BoxMesh longBarMesh = (BoxMesh)longBar.Mesh;
		longBarMesh.Size = new Vector3(baseLen+barWidth, barWidth, barWidth);
		float shft = 0.5f*(baseLen+barWidth) + 0.5f*barWidth;
		longBar.Position = new Vector3(shft, 0.0f, 0.0f);

		// left wheel
		MeshInstance3D tireL = 
			GetNode<MeshInstance3D>("CartFrame/WheelFrameL/Tire");
		TorusMesh tireMeshL = (TorusMesh)tireL.Mesh;
		tireMeshL.OuterRadius = wheelRad;
		tireMeshL.InnerRadius = wheelRad - tireWd;
		MeshInstance3D wheelL = 
			GetNode<MeshInstance3D>("CartFrame/WheelFrameL/Wheel");
		CylinderMesh wheelMeshL = (CylinderMesh)wheelL.Mesh;
		wheelMeshL.TopRadius = wheelRad - 0.5f*tireWd;
		wheelMeshL.BottomRadius = wheelRad - 0.5f*tireWd;
		wheelMeshL.Height = wheelWd;
		wheelFrameL.Position = new Vector3(0.0f, 0.0f, -0.5f*wheelSep);

		// right wheel
		MeshInstance3D tireR = 
			GetNode<MeshInstance3D>("CartFrame/WheelFrameR/Tire");
		TorusMesh tireMeshR = (TorusMesh)tireR.Mesh;
		tireMeshR.OuterRadius = wheelRad;
		tireMeshR.InnerRadius = wheelRad - tireWd;
		MeshInstance3D wheelR = 
			GetNode<MeshInstance3D>("CartFrame/WheelFrameR/Wheel");
		CylinderMesh wheelMeshR = (CylinderMesh)wheelR.Mesh;
		wheelMeshR.TopRadius = wheelRad - 0.5f*tireWd;
		wheelMeshR.BottomRadius = wheelRad - 0.5f*tireWd;
		wheelMeshR.Height = wheelWd;
		wheelFrameR.Position = new Vector3(0.0f, 0.0f, 0.5f*wheelSep);

		// Shift cart frame
		Node3D cartFrame = GetNode<Node3D>("CartFrame");
		cartFrame.Position = new Vector3(-cgDist, wheelRad, 0.0f);
	}

	//------------------------------------------------------------------------
	// Setters:
	//------------------------------------------------------------------------

	public float WheelRadius
	{
		set
		{
			if(value > 0.0f)
				wheelRad = value;
		}
	}

	public float SteeredWheelRadius
	{
		set{
			if(value > 0.0f)
				wheelRadS = value;
		}
	}

	public float WheelSeparation
	{
		set{
			if(value > 0.1f)
				wheelSep = value;
		}
	}

	public float BaseLength
	{
		set{
			if(value > 0.1f)
				baseLen = value;
		}
	}

	public float CasterLength
	{
		set{
			if(value > 0.0f)
				casterLen = value;
		}
	}

	public float TireWidth
	{
		set{
			if(value > 0.0f)
				tireWd = value;
		}
	}

	public float WheelWidth
	{
		set{
			if(value > 0.0f)
				wheelWd = value;
		}
	}

	public float SteeredTireWidth
	{
		set{
			if(value > 0.0f)
				tireWdS = value;
		}
	}

	public float FrameBarWidth
	{
		set{
			if(value > 0.0f)
				barWidth = value;
		}
	}
}
