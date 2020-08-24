using Monitoring.Core.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoring.Web.ViewModel
{
    public class DepartmentDashbordViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Url { get; set; }
        public RenderMode RenderMode { get; set; }
        public int WaitTime { get; set; }
        public int RefreshTime { get; set; }
        public byte[] Image { get; set; }

    }
}
