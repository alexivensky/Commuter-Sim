using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Commuter_Sim
{
    [GraphQLDescription("A simple train.")]
    public class Train : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        System.Timers.Timer? _timer;
        private const double INTERVAL = 1000;
        private const double TIME_DELTA = INTERVAL / 1000;

        private int _id;
        public bool IdSet;

        public Train() => new Train(0, 0, 0);

        public Train(double pos, double vel, double acc)
        {
            _position = pos;
            _velocity = vel;
            _acceleration = acc;

            _timer = new System.Timers.Timer(INTERVAL);
            _timer.Elapsed += UpdatePosition;
            _timer.Start();
        }

        public int ID
        {
            get => _id;
            set
            {
                if (!IdSet)
                {
                    _id = value;
                    IdSet = true;
                }
            }
        }

        private void UpdatePosition(object? sender, ElapsedEventArgs e)
        {
            _velocity += _acceleration * TIME_DELTA;
            _position += _velocity * TIME_DELTA;
          
        }

        //more thought should be put into these
        //especially their access modifiers

        private double _position;
        private double _velocity;
        private double _acceleration;

        // instantaneous properties
        [GraphQLDescription("Train's position.")]
        public double Position
        {
            get => _position;
            set
            {
                if (Math.Abs(value - _position) < 0.00001)
                {
                    return;
                } 
                _position = value;
                if (this is not null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }
        [GraphQLDescription("Train's velocity.")]
        public double Velocity
        {
            get => _velocity;
            set
            {
                if (Math.Abs(value - _velocity) < 0.00001)
                {
                    return;
                }
                _velocity = value;
                if (this is not null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Velocity)));
                }
            }
        }
        [GraphQLDescription("Train's acceleration.")]
        public double Acceleration
        {
            get => _acceleration;
            set
            {
                if (Math.Abs(value - _acceleration) < 0.00001)
                {
                    return;
                }
                _acceleration = value;
                if (this is not null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Acceleration)));
                }
            }
        }

        //intrinsic properties
        public double MaxSpeed { get; set; }
        public double Deceleration { get; set; }

        //things to calculate with each time step
        //note: these do not have set functions because that wouldn't make any sense
        public double DistanceTraveled { get; }
        public double DistanceToBrake { get; }

        
    }
}
