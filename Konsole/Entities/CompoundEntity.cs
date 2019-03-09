using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Entities
{
    /// <summary>
    /// An <see cref="Entity"/> that's built out of other Entities.
    /// </summary>
    public abstract class CompoundEntity
    {
        Entity[] Entities { get; set; }
    }
}
