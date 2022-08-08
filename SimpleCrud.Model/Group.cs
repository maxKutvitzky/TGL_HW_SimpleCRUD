using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleCrud.Model.Base;

namespace SimpleCrud.Model;

public class Group : BaseEntity
{
    [Required]
    [DisplayName("Group Name")]
    [StringLength(20)]
    public string Name { get; set; } = "NoName";

    public IEnumerable<Student> Students { get; set; } = new List<Student>();
}