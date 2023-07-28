using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    public class Subscription
    {
        [Subscribe]
        public Train TrainAdded([EventMessage] Train train) =>
            train;

        [Subscribe]
        public List<Train> GetTrains([EventMessage] Repository repository) =>
            repository.GetTrains();

        
    }
}
