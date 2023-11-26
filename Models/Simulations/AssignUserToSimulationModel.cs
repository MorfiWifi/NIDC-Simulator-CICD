using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Simulations
{
    public class AssignUserToSimulationModel
    {
        public Guid SimulationId { set; get; }
        public string Role { set; get; }
        public Guid UserId { set; get; }
    }
}
