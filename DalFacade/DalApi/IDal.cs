using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface IDal
    {
        public IEngineer Engineer { get; }
        public IDependence Dependence { get; }
        public ITask Task { get;}
    }
}
