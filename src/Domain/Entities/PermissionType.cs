using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityApi.Domain.Entities
{
    [Table("PermissionType", Schema = "dbo")]
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}