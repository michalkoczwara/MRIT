using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Mongoloid.Classes
{
    internal class ThreadedExecutor
    {
        public List<ShodanRecord> MongoHosts { get; set; }

        public List<RansomDemand> RansomDemands { get; set; }

        public List<RansomSchema> RansomSchemas { get; set; }

        public Dispatcher ParentDispatcher { get; set; }

        public string CurrentState { get; set; }

        public object Parent { get; set; }

        public ThreadedExecutor(Dispatcher dispatcher, object parent, List<RansomDemand> ransomDemands, List<RansomSchema> ransomSchemas)
        {
            CurrentState = "Created";
            MongoHosts = new List<ShodanRecord>();
            RansomDemands = ransomDemands;
            RansomSchemas = ransomSchemas;
            ParentDispatcher = dispatcher;
            Parent = parent;
        }

        public void StartGettingDetails()
        {
            CurrentState = "Started";
            foreach (var host in RansomDemands)
            {
                var ransomDemands = MongoFunctions.GetRansomDemands(host.Ip, host.Port, RansomSchemas);
                ThreadedFunctions.IncrementControlValue(ParentDispatcher, Parent, "RansomsFound");
                if (ransomDemands == null || !ransomDemands.Any())
                    continue;
                SqlFunctions.AddRansomDemands(ransomDemands);
                ThreadedFunctions.IncrementControlValue(ParentDispatcher, Parent, "ServersScanned");
            }
            CurrentState = "Stopped";
            ((MainWindow)Parent).CheckThreadStates();
        }
    }
}
