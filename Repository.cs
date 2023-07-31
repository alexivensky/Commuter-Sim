using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;

namespace Commuter_Sim
{
    public class Repository
    {
        private readonly ITopicEventSender _sender;

        public Repository(ITopicEventSender sender)
        {
            _sender = sender;
        }

        List<Train> trains = new List<Train>();
        public Task<List<Train>> GetTrainsAsync()
        {
            return Task.FromResult(trains);
        }
        public List<Train> GetTrains() => trains;
        public Task AddTrain(Train train)
        {
            trains.Add(train);
            return Task.CompletedTask;
        }

        
    }
}
