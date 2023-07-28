using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    public class Query //hello world
    {
        public Task<List<Train>> GetTrains([Service] Repository repository) =>
            repository.GetTrainsAsync();
    }
}
