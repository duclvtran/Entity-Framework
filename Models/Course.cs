using System.Collections.Generic;

namespace Models
{
    public partial class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; } = 3;
        public string Description { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}