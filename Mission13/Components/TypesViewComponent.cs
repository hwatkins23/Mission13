using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private BowlersDbContext _context { get; set; }
        public TypesViewComponent (BowlersDbContext temp)
        {
            _context = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["TeamName"];
            var bowlers = _context.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);
            return View(bowlers);
        }
    }
}
