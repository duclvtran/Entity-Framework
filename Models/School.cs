using System.Collections.Generic;

namespace Models
{
    public partial class School
    {
        public School()
        {
            this.Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
