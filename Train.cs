using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using HotChocolate.Subscriptions;

namespace Commuter_Sim
{
    [GraphQLDescription("A simple train.")]
    public class Train
    {
        private const double INTERVAL = 10;
        private const double TIME_DELTA = INTERVAL / 1000;
        private const double DWELL_TIME = 5000;
        private int _id;

        public Train(double pos, double vel, double acc, int id)
        {
            _position = pos;
            _velocity = vel;
            _acceleration = acc;

            _id = id;
        }

        public int ID => _id;

        public void UpdatePosition()
        {
            if (this is null) return;
            Velocity += Acceleration * TIME_DELTA;
            Position += Velocity * TIME_DELTA;
        }

        //more thought should be put into these
        //especially their access modifiers

        private double _position;
        private double _velocity;
        private double _acceleration;

        private double _distanceToTravel;

        // instantaneous properties
        [GraphQLDescription("Train's position.")]
        public double Position
        {
            get => _position;
            set
            {
                if (Math.Abs(value - _position) < 0.001)
                {
                    return;
                } 
                _position = value;
                
            }
        }
        [GraphQLDescription("Train's velocity.")]
        public double Velocity
        {
            get => _velocity;
            set
            {
                if (Math.Abs(value - _velocity) < 0.001)
                {
                    return;
                }
                _velocity = value;
            }
        }
        [GraphQLDescription("Train's acceleration.")]
        public double Acceleration
        {
            get => _acceleration;
            set
            {
                if (Math.Abs(value - _acceleration) < 0.001)
                {
                    return;
                }
                _acceleration = value;
            }
        }

        public double DistanceToTravel
        {
            get => _distanceToTravel;
            set => _distanceToTravel = value;
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
