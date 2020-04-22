using System.Collections.Generic;

namespace Models
{
    public partial class Student : Person
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        public string Group { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
