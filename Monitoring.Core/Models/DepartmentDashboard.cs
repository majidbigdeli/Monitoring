using Monitoring.Core.Enums;
using System;

namespace Monitoring.Core.Models
{
    public class DepartmentDashboard
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Url { get; set; }
        public RenderMode RenderMode { get; set; }
        public int WaitTime { get; set; }
        public int RefreshTime { get; set; }
        public byte[] Image { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual Department Department { get; set; }

    }
}
