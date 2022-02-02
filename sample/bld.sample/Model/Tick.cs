using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bld.sample.Model
{
    internal class Tick
    {
        internal Guid Id { get; set; } = Guid.NewGuid();
        internal DateTimeOffset TickTime { get; set; } = DateTimeOffset.Now;
    }
}
