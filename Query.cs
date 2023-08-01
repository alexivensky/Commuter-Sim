using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commuter_Sim
{
    /**
     * Client-side queries.
     * Can be used to manually retrieve data once at a time.
     * Not used on server side.
     */
    public class Query
    {
        public Task<List<Train>> GetTrains([Service] Repository repository) =>
            repository.GetTrainsAsync();
    }
}
