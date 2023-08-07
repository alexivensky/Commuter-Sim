using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    public class Station
    {
        private List<Train> _trainsAtStation = new();
        private string _stationName;

        public Station(string StationName)
        {
            _stationName = StationName;
        }

        public List<Train> TrainsAtStation
        {
            get => _trainsAtStation;
        }

        public string StationName
        {
            get => _stationName;
            set => _stationName = value;
        }
    }
}
