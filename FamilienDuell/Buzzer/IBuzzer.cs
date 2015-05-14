using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilienDuell
{
    public interface IBuzzer
    {
        event EventHandler<BuzzeredEventArgs> Buzzered;
    }
}
