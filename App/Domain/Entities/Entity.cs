using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Domain.Entities
{
    public abstract class Entity : ObservableObject
    {
        protected Entity()
        {

        }

        protected Entity(string name)
        {
            Name = name;
        }

        [PrimaryKey, Indexed, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

    }
}
