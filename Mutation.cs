using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;

namespace Commuter_Sim
{
    public class Mutation
    {
        public async Task<TrainPayload> AddTrain(TrainInput input, [Service] Repository repository, [Service] ITopicEventSender sender)
        {
            await sender.SendAsync(nameof(Subscription.TrainAdded), new Train(input.pos, input.vel, input.acc, sender));
            var train = new Train(input.pos, input.vel, input.acc, sender);
            await repository.AddTrain(train);
            return new TrainPayload(train);
        }


        public record TrainPayload(Train record);
        public record TrainInput(double pos, double vel, double acc);
    }
}
