using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bld.sample.Model
{
    public class Tick
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset TickTime { get; set; } = DateTimeOffset.Now;
    }
}
