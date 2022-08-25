using SimpleCrud.Model.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrud.Model
{
    public class Passport : BaseEntity
    {
        public int StudentId { get; set; }
        [Column(TypeName = "Date")]
        [DisplayName("Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public Student Student { get; set; }

    }
}
