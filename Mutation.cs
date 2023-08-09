using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;

namespace Commuter_Sim
{
    /**
     * Client-Side Mutations.
     * Can be used through the HotChocolate IDE to add, remove, or modify objects.
     * Cannot be used on the server side.
     */

    public class Mutation
    {
        private String? _errorMessage;
        private int _errorFlag;
        public async Task<Train> AddTrain(TrainInput input, [Service] Repository repository, [Service] ITopicEventSender sender)
        {
            _errorFlag = 0;
            _errorMessage = "";
            if (input.pos < 0)
            {
                _errorMessage += "Position cannot be negative. ";
                _errorFlag++;
            }
            if (repository.IDExists(input.id))
            {
                _errorMessage += $"A train with ID: {input.id} already exists. Each train must have a unique ID. ";
                _errorFlag++;
            }
            if (input.deceleration > 0)
            {
                _errorMessage += $"Deceleration must be a negative number. ";
                _errorFlag++;
            }
            if (input.maxSpeed <= 0)
            {
                _errorMessage += $"Maximum speed must be a positive, non-zero number. ";
                _errorFlag++;
            }

            if (_errorFlag > 0)
            {
                throw new ApplicationException($"{_errorFlag} Errors: " + _errorMessage);
            }

            Train train = new Train(input.pos, input.vel, input.acc, input.maxSpeed, input.totalDistance, input.deceleration, input.id);
            await repository.AddTrain(train);
            await sender.SendAsync(nameof(Subscription.TrainAdded), train);
            await sender.SendAsync(nameof(Subscription.GetTrains), repository);
            return train;
        }

        public async Task<Train> RemoveTrain(int id, [Service] Repository repository, [Service] ITopicEventSender sender)
        {
            Train? temp = repository.GetTrain(id);
            if (!repository.IDExists(id) || temp is null)
            {
                throw new ApplicationException($"Train with ID: {id} does not exist.");
            }
            await repository.RemoveTrain(id);
            await sender.SendAsync(nameof(Subscription.GetTrains), repository);
            await sender.SendAsync(nameof(Subscription.RemovedTrain), temp);
            return temp;
        }

        public async Task<String> PauseTimer([Service] Repository repository)
        {
            repository.PauseTimer();
            return "Timer paused.";
        }

        public async Task<String> ResumeTimer([Service] Repository repository)
        {
            repository.StartTimer();
            return "Timer resumed.";
        }
        public record TrainInput(double pos, double vel, double acc, double maxSpeed, double totalDistance, double deceleration, int id);
    }
}
