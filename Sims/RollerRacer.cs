//============================================================================
// RollerRacer.cs : Defines derived class for simulating a Roller Racer.
//       Equations of motion are derived in class notes.
//============================================================================
using System;

public class RollerRacer : Simulator
{
    // physical parameters, names are the same as that in the notes
    double m;   // mass of vehicle
    double Ig;  // moment of inertia (vertical axis) about center of mass
    double b;   // distance of com ahead of rear axle
    double c;   // distance of rear contact patch from symmetry axis
    double d;   // caster length
    double h;   // longitudinal distance between center of mass and steer axis

    double rW;  // radius of rear wheel, used for calculating rotation rate
    double rWs; // radius of steered wheel, used for calculating rotation rate

    double kPDelta;  // proportional gain for steer filter
    double kDDelta;  // derivative gain for steer filter

    public RollerRacer() : base(11)
    {
        SetInertia(25.0 /*mass*/, 0.3 /*radius of gyration*/);

        x[0] = 0.0;   // x coordinate of center of mass
        x[1] = 0.0;   // xDot, time derivative of x
        x[2] = 0.0;   // z coordinate of center of mass
        x[3] = 0.0;   // zDot, time derivative of z
        x[4] = 0.0;   // psi, heading angle
        x[5] = 0.0;   // psiDot, time derivative of heading, yaw rate
        x[6] = 0.0;   // rotation angle of left rear wheel
        x[7] = 0.0;   // rotation angle of right rear wheel
        x[8] = 0.0;   // rotation angle of front steered wheel
        x[9] = 0.0;   // delta, steer angle
        x[10] = 0.0;  // deltaDot, steer rate

        SetRHSFunc(RHSFuncRRacer);
    }

    private void RHSFuncRRacer(double[] xx, double t, double[] ff)
    {
        // give names to some state variable so code is easier to read & write
        double xDot = xx[1];
        double yDot = xx[3];
        double psi  = xx[4];
        double psiDot = xx[5];
        double delta = xx[9];
        double deltaDot = xx[10];

        // calculate some trig functions here, so you only have to do it once
        double cosPsi = Math.Cos(psi);
        double sinPsi = Math.Sin(psi);
        double cosPsiPlusDelta = Math.Cos(psi + delta);
        double sinPsiPlusDelta = Math.Sin(psi + delta);

        // #### You will do some hefty calculations here

        // #### Right sides are zero for now. You will fix
        ff[0] = 0.0;
        ff[1] = 0.0;
        ff[2] = 0.0;
        ff[3] = 0.0;
        ff[4] = 0.0;
        ff[5] = 0.0;
        ff[6] = 0.0;
        ff[7] = 0.0;
        ff[8] = 0.0;
        ff[9] = 0.0;
        ff[10] = 0.0;
    }

    //------------------------------------------------------------------------
    // SetInertia: sets the two inertia properties of the vehicle. 
    //     mm: total mass in kilograms
    //     rgyr: radius of gyration in meters
    //------------------------------------------------------------------------
    public void SetInertia(double mm, double rgyr)
    {
        if(mm <= 0.1)   // check lower bound for mass
            return;     // return and not update parameters.

        if(rgyr < 0.03) // check lower bound for radius of gyration
            return;     // return and not update parameters.

        m = mm;
        Ig = m*rgyr*rgyr;
    }
}