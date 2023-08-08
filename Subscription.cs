using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
        public List<Train> GetTrains([EventMessage] Repository repository)
        {
            return repository.GetTrains();
        }

        [Subscribe]
        [Topic]
        public Train OnTrainPositionUpdated([EventMessage] Train train) => 
            train;

        [Subscribe]
        public List<Train> OnTrainsUpdated([EventMessage] List<Train> trains) =>
            trains;
    }
}
