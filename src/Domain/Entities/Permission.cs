using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityApi.Domain.Entities
{
    [Table("Permission", Schema = "dbo")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(PermissionType))]
        public int PermissionTypeId { get; set; }

        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [MaxLength(100)]
        public string EmployeeLastName { get; set; }

        public PermissionType PermissionType { get; set; }
    }
}