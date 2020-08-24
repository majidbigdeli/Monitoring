using System.Collections.Generic;

namespace Monitoring.Web.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public List<DepartmentDashbordViewModel> DepartmentDashbords { get; set; }
    }
}
