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
        public async Task<TrainPayload> AddTrain(TrainInput input, [Service] Repository repository, [Service] ITopicEventSender sender)
        {
            var train = new Train(input.pos, input.vel, input.acc, input.id);
            await repository.AddTrain(train);
            await sender.SendAsync(nameof(Subscription.TrainAdded), train);
            await sender.SendAsync(nameof(Subscription.GetTrains), repository);
            return new TrainPayload(train);
        }


        public record TrainPayload(Train train);
        public record TrainInput(double pos, double vel, double acc, int id);
    }
}
