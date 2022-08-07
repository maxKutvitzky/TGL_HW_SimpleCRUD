using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleCrud.Model.Base;

namespace SimpleCrud.Model
{
    public class Group : BaseEntity
    {
        [Required]
        [DisplayName("Group")]
        [StringLength(20)]
        public string Name { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
