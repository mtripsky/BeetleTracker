using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeetleTracker.Data;
using BeetleTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeetleTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IEntityBaseRepository<Project> _repository;

        public ProjectsController(IEntityBaseRepository<Project> repository)
        {
            _repository = repository;
        }

        // GET: Projects/
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create/:project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/:id
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

        // POST: Projects/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _repository.Update(id, project);
                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

        // GET: Projects/Delete/:id
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

        // POST: Projects/Delete/:id
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
    }
}
