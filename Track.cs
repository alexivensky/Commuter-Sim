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

        public Track(int trackLength)
        {
            _trackLength = trackLength;
        }

        public List<Train> TrainsOnTrack => _trainsOnTrack;
        public int TrackLength => _trackLength;
    }
}
