using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    [GraphQLDescription("A simple train.")]
    public class Train : INotifyPropertyChanged
    {
        private static System.Timers.Timer? timer;
        private const double INTERVAL = 1000;
        private const double TIME_DELTA = 1 / INTERVAL;
        public Train() => new Train(0, 0, 0);

        public Train(double pos, double vel, double acc)
        {
            Position = pos;
            Velocity = vel;
            Acceleration = acc;
        }

        private void UpdatePosition(object? sender, PropertyChangedEventArgs e)
        {

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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
