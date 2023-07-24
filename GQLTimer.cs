using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using HotChocolate.Subscriptions;
using static Commuter_Sim.Mutation;

namespace Commuter_Sim
{
    public class GQLTimer
    {
        private static System.Timers.Timer? timer;

        private int _interval;
        public GQLTimer(int interval)
        {
            timer = new System.Timers.Timer(interval);
        }

        public static void SetTimer([Service] ITopicEventSender S1, [Service] Repository repository)
        {
            timer.Elapsed += async(S1, e) => await HandleTimer(S1, repository);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        
        private static Task HandleTimer(ITopicEventSender sender, Repository repository)
        {
            Mutation m = new Mutation();

            //???

            return Task.CompletedTask;

            
        }
        

        public Task TimerAdd([Service] Repository repository, [Service] ITopicEventSender sender)
        {
            sender.SendAsync(nameof(Subscription.TrainAdded), new Train());
            repository.AddTrain(new Train());
            return Task.CompletedTask;
        }
        
    }
}
