using Issue_tracker.Data;
using Issue_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Issue_tracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Project project)
        {
            if(!ModelState.IsValid)
            {
                return View(project);
            }
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
