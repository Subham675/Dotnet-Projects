using Issue_tracker.Models;
using Issue_tracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Issue_tracker.Controllers;

public class IssuesController : Controller
{
    private readonly AppDbContext _context;

    public IssuesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string? search , Status? status , int? projectId)
    {
        IQueryable<Issue> query = _context.Issues
         .Include(i => i.Project)
         .Include(i => i.Assignee);
        if (projectId != null)
        {
            query = query.Where(i => i.ProjectId == projectId);
        }
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(i => i.Title.Contains(search));
        }
        if(status != null)
        {
            query = query.Where(i => i.Status == status);
        }

        var issues = query.OrderByDescending(i => i.CreatedAt).ToList();
        ViewBag.Search = search;
        return View(issues);
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var issue = _context.Issues.Find(id);
        if(issue != null)
        {
            _context.Issues.Remove(issue);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");

    }
    [HttpPost]
    public IActionResult Create(Issue issue)
    {
        ModelState.Remove("Project");
        ModelState.Remove("Reporter");

        if (!ModelState.IsValid)
        {
            ViewBag.Projects = _context.Projects
                .Select(p => new SelectListItem 
                { Value = p.Id.ToString(), Text = p.Name }).ToList();
            ViewBag.Users = _context.AppUsers.
                 Select(u => new SelectListItem 
                 { Value = u.Id.ToString(), Text = u.Name }).ToList();
                return View(issue);
        }

        issue.CreatedAt = DateTime.UtcNow;
        _context.Issues.Add(issue);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    public IActionResult Create()
    {
        ViewBag.Projects = _context.Projects.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
        ViewBag.Users = _context.AppUsers.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
        return View();
    }
    public IActionResult Edit(int id)
    {
        var issue = _context.Issues.Find(id);
        if(issue == null)
        {
            return NotFound(); 
        }
        ViewBag.Projects = _context.Projects.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
        ViewBag.Users = _context.AppUsers.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
        return View(issue);
    }
    [HttpPost]
    public IActionResult Edit(int id ,Issue issue)
    {
        var existing = _context.Issues.Find(id);
        if (existing == null)
        {
            return NotFound();
        }
        existing.Title = issue.Title;
        existing.Description = issue.Description;
        existing.ProjectId = issue.ProjectId;
        existing.ReporterId = issue.ReporterId;
        existing.AssigneeId = issue.AssigneeId;
        existing.Priority = issue.Priority;
        existing.Status = issue.Status;
        _context.SaveChanges();


        return RedirectToAction("Index");
    }
    public IActionResult Details(int id)
    {
        var issue = _context.Issues
        .Include(i => i.Project)
        .Include(i => i.Reporter)
        .Include(i => i.Assignee)
        .Include(i=>i.Comments)
        .ThenInclude(c=>c.Author)
        .FirstOrDefault(i=>i.Id == id);
        if(issue == null)
        {
            return NotFound();
        }
        return View(issue);
    }

    [HttpPost]
    public IActionResult AddComment(int issueId, string text)
    {
        var comment = new Comment();
        comment.Text = text;
        comment.AuthorId = 1;
        comment.IssueId = issueId;
        comment.CreatedAt = DateTime.UtcNow;
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return RedirectToAction("Details", new { id = issueId });
    }
    
    [HttpPost]
    public IActionResult DeleteComment(int id,int issueId)
    {
        var comment = _context.Comments.Find(id);
        if (comment!=null)
        {
            _context.Comments.Remove(comment);
        }
        _context.SaveChanges();
        return RedirectToAction("Details", new { id = issueId });
    }
    [HttpPost]
    public IActionResult ChangeStatus(int id, Status status)
    {
        var issue = _context.Issues.Find(id);
        if (issue==null)
        {
            return NotFound();
        }
        issue.Status = status;2
        _context.SaveChanges();

        return RedirectToAction("Details", new {id=id});
    }

}