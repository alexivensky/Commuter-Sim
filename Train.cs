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
        private const int DWELL_TIME = 100;
        private const double CREEP_SPEED = 0.2;
        private int _id;

        public Train(double position, double velocity, double acceleration, double maxSpeed, double deceleration, int id)
        {
            _position = position;
            _velocity = velocity;
            _acceleration = acceleration;
            _maxSpeed = maxSpeed;
            _deceleration = deceleration;
            _initAcceleration = acceleration;
            _id = id;
            _status = "";
            _brakingFlag = false;
            _stoppedFlag = false;
            _creepingFlag = false;
            _doneFlag = false;

            _currentStation = new Station("A");
            _atStation = true;
            _route.Add(_currentStation);
            _route.Add(new Track(50));
            _route.Add(new Station("B"));

            Track temp = (Track)_route[1];
            _totalDistance = temp.TrackLength;

            _routeIndex = 0;

            _dwellCounter = DWELL_TIME;
        }

        private double _initAcceleration;

        public int ID => _id;

        public void UpdatePosition(double TIME_DELTA)
        {
            if (this is null) return;

            

            if (_atStation) // at station
            {
                _status = "DWELLING";
                //set all private members to values of next track
                _position = 0;
                if (_dwellCounter > 0 && _atStation)
                {
                    _dwellCounter--;
                }
                else
                {
                    _atStation = false;
                    _routeIndex++;
                    if (_routeIndex == _route.Count)
                    {
                        _status = "SIMULATION DONE. A strange game. The only winning move is not to play. How about a nice game of chess?";
                        return;
                    }
                    else if (_routeIndex < _route.Count && _route[_routeIndex] is Track)
                    {
                        Track temp = (Track)_route[_routeIndex];
                        _totalDistance = temp.TrackLength;
                        _position = 0;
                    }
                    Acceleration = _initAcceleration;
                }
            }

                if (!_atStation)
                {
                    Velocity += Acceleration * TIME_DELTA;
                    Position += Velocity * TIME_DELTA;

                    if (Acceleration > 0)
                    {
                        _status = "ACCELERATING";
                    }

                    _distanceToTravel = _totalDistance - Position;

                    _distanceToBrake = -(Math.Pow(Velocity, 2)) / (2 * Deceleration);

                    if (Math.Abs(_distanceToBrake - _distanceToTravel) <= 1 && !_stoppedFlag && !_creepingFlag)
                    {
                        _acceleration = Deceleration;
                        _brakingFlag = true;
                        _status = "BRAKING";
                    }

                    if (Math.Abs(_velocity) <= 0.1 && _brakingFlag || _creepingFlag)
                    {
                        Acceleration = 0;
                        if (_distanceToTravel > 0)
                        {
                            Velocity = CREEP_SPEED;
                            _status = "CREEPING";
                            _creepingFlag = true;
                            _brakingFlag = false;
                        }
                        else
                        {
                            Velocity = 0;
                            _stoppedFlag = true;
                            _creepingFlag = false;
                            _dwellCounter = DWELL_TIME;
                            _atStation = true;
                            _routeIndex++;
                        }
                    }

                    if (Velocity >= MaxSpeed && !_brakingFlag && !_stoppedFlag)
                    {
                        Acceleration = 0;
                        Velocity = MaxSpeed;
                        _status = "MAX SPEED REACHED";
                    }
                }
        }

        private double _position;
        private double _velocity;
        private double _acceleration;
        private double _maxSpeed;
        private double _totalDistance;
        private double _distanceToTravel;
        private double _deceleration;
        private double _distanceToBrake;

        private Station? _currentStation;
        private Station? _origin;
        private Station? _destination;

        private Track? _currentTrack;

        private List<object> _route = new List<object>();
        private int _routeIndex;

        private bool _brakingFlag;
        private bool _stoppedFlag;
        private string _status;
        private bool _creepingFlag;
        private bool _doneFlag;

        private bool _atStation;

        private int _dwellCounter;

        // instantaneous properties
        [GraphQLDescription("Train's position.")]
        public double Position
        {
            get => _position;
            set => _position = value;
        }
        [GraphQLDescription("Train's velocity.")]
        public double Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }
        [GraphQLDescription("Train's acceleration.")]
        public double Acceleration
        {
            get => _acceleration;
            set => _acceleration = value;
        }

        public double MaxSpeed
        {
            get => _maxSpeed;
        }

        public double DistanceToTravel
        {
            get => _distanceToTravel;
            set => _distanceToTravel = value;
        }

        //intrinsic properties
        public double Deceleration
        {
            get => _deceleration;
        }

        public double TotalDistance
        {
            get => _totalDistance;
            set => _totalDistance = value;
        }

        //things to calculate with each time step
        //note: these do not have set functions because that wouldn't make any sense
        public double DistanceTraveled { get; }
        public double DistanceToBrake => _distanceToBrake;

        public String Status => _status;

        public double DwellCounter
        {
            get => _dwellCounter / 100.0;
        }

        public bool DoneFlag
        {
            get => _doneFlag;
        }
    }
}
