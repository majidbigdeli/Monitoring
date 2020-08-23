using Monitoring.Core.Enums;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public RenderStrategy RenderStrategy { get; set; }
        public virtual ICollection<DepartmentDashboard> DepartmentDashbords { get; set; }
    }
}
