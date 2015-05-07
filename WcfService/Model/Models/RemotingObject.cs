using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class RemotingObject : MarshalByRefObject
    {
        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            return lease;
        }

        public string Id { get; set; }
        public string Serial { get; set; }
        public Action Action { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public string Remarks { get; set; }
    }
}
