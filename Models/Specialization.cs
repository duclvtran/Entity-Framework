using System.Collections.Generic;

namespace Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
