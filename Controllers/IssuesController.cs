using System;
using System.Linq;
using BeetleTracker.Data;
using BeetleTracker.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BeetleTracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IEntityBaseRepository<Issue> _repository;
        private readonly int _pageSize = 10;  // should be in constructor
        private int _pageIndex;  // should be in constructor

        public IssuesController(IEntityBaseRepository<Issue> repository)
        {
            _repository = repository;
        }

        // GET: Issues/
        public IActionResult Index(int? page, [FromQuery(Name = "sortBy")] string sortBy)
        {
            _pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CreatedSort = String.IsNullOrEmpty(sortBy) ? "Created desc" : "";
            ViewBag.UpdatedSort = sortBy == "Updated" ? "Updated desc" : "Updated";
            ViewBag.StatusSort = sortBy == "Status" ? "Status desc" : "Status";
            ViewBag.ReporterSort = sortBy == "Reporter" ? "Reporter desc" : "Reporter";
            ViewBag.PrioritySort = sortBy == "Priority" ? "Priority desc" : "Priority";

            var issues = _repository.GetAll();

            switch (sortBy)
            {
                case "Updated desc":
                    issues = issues.OrderByDescending(x => x.Updated);
                    break;
                case "Updated":
                    issues = issues.OrderBy(x => x.Updated);
                    break;
                case "Reporter desc":
                    issues = issues.OrderByDescending(x => x.Reporter);
                    break;
                case "Priority":
                    issues = issues.OrderBy(x => x.Priority);
                    break;
                case "Priority desc":
                    issues = issues.OrderByDescending(x => x.Priority);
                    break;
                case "Reporter":
                    issues = issues.OrderBy(x => x.Reporter);
                    break;
                case "Status desc":
                    issues = issues.OrderByDescending(x => x.Status);
                    break;
                case "Status":
                    issues = issues.OrderBy(x => x.Status);
                    break;
                case "Created desc":
                    issues = issues.OrderByDescending(x => x.Created);
                    break;
                default:
                    issues = issues.OrderBy(x => x.Created);
                    break;
            }
            return View(issues.ToPagedList(_pageIndex, _pageSize));
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create/:project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Issue issue)
        {
            var timeNow = DateTime.Now;
            issue.Created = timeNow;
            issue.Updated = timeNow;

            if (ModelState.IsValid)
            {
                _repository.Create(issue);
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/:id
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = _repository.GetSingle(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }
            var oldIssue = _repository.GetSingle(id);
            issue.Updated = DateTime.Now;
            issue.Created = oldIssue.Created;

            if (ModelState.IsValid)
            {
                _repository.Update(id, issue);
                return RedirectToAction(nameof(Index));
            }

            return View(issue);
        }

        // GET: Issues/Delete/:id
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = _repository.GetSingle(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var issue = _repository.GetSingle(id);

                if (issue == null)
                {
                    return NotFound();
                }

                _repository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Issues/Details/:id
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = _repository.GetSingle(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }
    }
}
