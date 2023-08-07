using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    public class Track
    {
        private List<Train> _trainsOnTrack = new List<Train>();
        private int _trackLength;

        private Station _stationA, _stationB;

        public Track(int trackLength, Station stationA, Station stationB)
        {
            _trackLength = trackLength;
            _stationA = stationA;
            _stationB = stationB;
        }
    }
}
