using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlersDbContext _context { get; set; }

        public HomeController(BowlersDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index(string TeamName)
        {
            object blah = null;
            if(TeamName != null)
            {
                blah = _context.Bowlers.Where(x => x.TeamName.TeamName == TeamName).Include(x => x.TeamName).ToList();
            }
            else 
            {
                blah = _context.Bowlers.Include(x => x.TeamName).ToList();
            }
            
            return View(blah);
        }
        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            int id = _context.Bowlers.Count();
            id++;
            b.BowlerID = id;
            _context.Add(b);
            _context.SaveChanges();
            return View("Confirmation", b);
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _context.Teams.ToList();
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View("AddBowler", bowler);
        }
        [HttpPost] 
        public IActionResult Edit(Bowler b)
        {
            _context.Update(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet] 
        public IActionResult Delete(int bowlerid)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View(bowler);
        }
        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _context.Bowlers.Remove(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
