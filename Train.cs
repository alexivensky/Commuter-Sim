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
        public ITopicEventSender _sender;
        System.Timers.Timer? _timer;
        private const double INTERVAL = 10;
        private const double TIME_DELTA = INTERVAL / 1000;

        private int _id;
        public bool IdSet;

        

        public Train(double pos, double vel, double acc, ITopicEventSender sender)
        {
            _position = pos;
            _velocity = vel;
            _acceleration = acc;

            _sender = sender;

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
            if (this is null) return;
            Velocity += Acceleration * TIME_DELTA;
            Position += Velocity * TIME_DELTA;
            _sender.SendAsync(nameof(Subscription.OnTrainPositionUpdated), this);
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

        //intrinsic properties
        public double MaxSpeed { get; set; }
        public double Deceleration { get; set; }

        //things to calculate with each time step
        //note: these do not have set functions because that wouldn't make any sense
        public double DistanceTraveled { get; }
        public double DistanceToBrake { get; }

        
    }
}
