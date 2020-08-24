using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monitoring.Core;
using Monitoring.Core.Models;
using Monitoring.Web.Mapper;
using Monitoring.Web.ViewModel;

namespace Monitoring.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MonitoringContext _context;

        public HomeController(MonitoringContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string key)
        {
            var dashbord = await _context.Set<Department>().Include(x => x.DepartmentDashbords).FirstOrDefaultAsync(x => x.Key == key);

            if (dashbord == null)
            {
                return RedirectToAction(nameof(NotFound), new { key = key });
            }

            var vm = dashbord.ToViewModel();

            return View(vm);
        }


        public IActionResult NotFound(string key)
        {
            var vm = new NotFoundViewModel()
            {
                Key = key
            };
            return View(vm);
        }
    }
}
