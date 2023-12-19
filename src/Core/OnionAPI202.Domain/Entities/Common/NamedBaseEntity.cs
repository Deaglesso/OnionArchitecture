using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Domain.Entities.Common
{
    public abstract class NamedBaseEntity:BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
