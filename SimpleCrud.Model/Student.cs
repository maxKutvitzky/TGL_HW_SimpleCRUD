using SimpleCrud.Model.Base;
using SimpleCrud.Model.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrud.Model;

public class Student : BaseEntity
{
    [Required]
    [DisplayName("First Name")]
    [StringLength(20)]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Last Name")]
    [StringLength(20)]
    public string LastName { get; set; }

    [Required] public GenderEnum Gender { get; set; }

    [Required]
    [DisplayName("Phone")]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required][EmailAddress] public string Email { get; set; }

    [Required][DisplayName("Group")] public Group Group { get; set; }

    [Required] public PaymentPlanEnum PaymentPlan { get; set; }
    public IEnumerable<Subject> Subjects { get; set; }
    public Passport Passport { get; set; }
}