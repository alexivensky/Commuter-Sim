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
    /**
     * Simple container class for server objects.
     */
    public class Repository
    {
        private readonly ITopicEventSender _sender;
        private System.Timers.Timer _timer;
        private const double INTERVAL = 10;
        private const double TIME_DELTA = INTERVAL / 1000;

        public Repository(ITopicEventSender sender)
        {
            _sender = sender;

            _timer = new System.Timers.Timer(INTERVAL);
            _timer.Elapsed += RepositoryTimerHandler;
            _timer.Start();
        }

        private void RepositoryTimerHandler(object? sender, ElapsedEventArgs e)
        {
            foreach (Train train in trains)
            {
                train.UpdatePosition(TIME_DELTA);
            }
            _sender.SendAsync(nameof(Subscription.OnTrainsUpdated), trains);
        }

        private List<Train> trains = new List<Train>();

        public bool IDExists(int check)
        {
            return trains.Exists(train => train.ID == check);
        }

        public Task<List<Train>> GetTrainsAsync() => 
            Task.FromResult(trains);

        public List<Train> GetTrains() => 
            trains;

        public Task AddTrain(Train train)
        {
            trains.Add(train);
            return Task.CompletedTask;
        }
    }
}
