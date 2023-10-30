using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Domain.Entities
{
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }
    }
}
