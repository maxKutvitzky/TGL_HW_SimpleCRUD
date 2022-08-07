using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleCrud.Model.Base;
using SimpleCrud.Model.Enums;

namespace SimpleCrud.Model
{
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
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        [DisplayName("Phone")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Group? Group { get; set; }
        [Required]
        public PaymentPlan PaymentPlan { get; set; }

    }
}
