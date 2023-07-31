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

        public void TrainPropertyChange(Train train)
        {
            train.PropertyChanged += TrainPropertyChanged;
        }

        private void TrainPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Train train)
            {
                switch (e.PropertyName)
                {
                    case "Position":
                        //send event to HotChocolate subscription
                        _sender.SendAsync(nameof(Subscription.TrainAdded), train);
                        break;
                    case "velocity":
                        break;
                    case "Acceleration":
                        break;
                }
                    
            }
            
        }
    }
}
