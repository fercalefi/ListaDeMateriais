using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orcamento.Models
{
    public class Materiais
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Materiais()
        {

        }

        public Materiais(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
