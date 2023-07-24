using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    [GraphQLDescription("A simple train.")]
    public class Train
    {
        public Train()
        {
            Position = 0;
            Velocity = 0;
            Acceleration = 0;
        }

        public Train(double pos, double vel, double acc)
        {
            Position = pos;
            Velocity = vel;
            Acceleration = acc;
        }



        //more thought should be put into these
        //especially their access modifiers

        // instantaneous properties
        [GraphQLDescription("Train's position.")]
        public double Position { get; set; }
        [GraphQLDescription("Train's velocity.")]
        public double Velocity { get; set; }
        [GraphQLDescription("Train's acceleration.")]
        public double Acceleration { get; set; }

        //intrinsic properties
        public double MaxSpeed { get; set; }
        public double Deceleration { get; set; }

        //things to calculate with each time step
        //note: these do not have set functions because that wouldn't make any sense
        public double DistanceTraveled { get; }
        public double DistanceToBrake { get; }

    }
}
