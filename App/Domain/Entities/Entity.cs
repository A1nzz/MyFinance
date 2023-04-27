using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        protected Entity(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

    }
}
