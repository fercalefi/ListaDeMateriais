using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orcamento.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ser entre {2} e {1}")]
        [Display(Name="Nome")]
        public string Name { get; set; }
        public string Endereco { get; set; }

        [Display(Name = "Fone")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(00) 0000-0000}")]
        public string Telefone { get; set; }

        public Empresa()
        {
        }

        public Empresa(int id, string name, string endereco, string telefone)
        {
            Id = id;
            Name = name;
            Endereco = endereco;
            Telefone = telefone;
        }
    }
}
