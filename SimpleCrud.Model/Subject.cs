using SimpleCrud.Model.Base;

namespace SimpleCrud.Model
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
