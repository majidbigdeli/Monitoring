using Monitoring.Core.Models;
using Monitoring.Web.ViewModel;
using System;
using System.Linq;

namespace Monitoring.Web.Mapper
{
    public static class DepratmentMapper
    {
        public static DepartmentViewModel ToViewModel(this Department department)
        {
            return new DepartmentViewModel()
            {
                Id = department.Id,
                Key = department.Key,
                Name = department.Name,
                DepartmentDashbords = department.DepartmentDashbords.Select(x => new DepartmentDashbordViewModel()
                {
                    Id = x.Id,
                    DepartmentId = x.DepartmentId,
                    Image = x.Image,
                    RefreshTime = x.RefreshTime,
                    RenderMode = x.RenderMode,
                    Url = x.RenderMode == Core.Enums.RenderMode.Html ? x.Url : Convert.ToBase64String(x.Image),
                    WaitTime = x.WaitTime
                }).ToList()
            };

        }
    }
}
