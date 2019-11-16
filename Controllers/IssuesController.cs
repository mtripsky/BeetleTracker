using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeetleTracker.Data;
using BeetleTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeetleTracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IEntityBaseRepository<Issue> _repository;

        public IssuesController(IEntityBaseRepository<Issue> repository)
        {
            _repository = repository;
        }

        // GET: Issues/
        public IActionResult Index()
        {
            return View(_repository.GetAll());
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

            var project = _repository.GetSingle(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
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
            else
            {
                return View(issue);
            }
        }

        // GET: Issues/Delete/:id
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _repository.GetSingle(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Issues/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var project = _repository.GetSingle(id);

                if (project == null)
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
