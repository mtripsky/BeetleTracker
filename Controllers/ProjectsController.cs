using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeetleTracker.Data;
using BeetleTracker.Models.Entities;
using BeetleTracker.Models.ProjectViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeetleTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IEntityBaseRepository<Project> _projectRepo;
        private readonly IEntityBaseRepository<ApplicationUser> _userRepo;
        private readonly IMapper _mapper;

        public ProjectsController(
            IEntityBaseRepository<Project> projectRepo,
            IEntityBaseRepository<ApplicationUser> userRepo,
            IMapper mapper)
        {
            _projectRepo = projectRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        // GET: Projects/
        public IActionResult Index()
        {
            var projects = _projectRepo
                            .GetAll()
                            .Select(p => _mapper.Map<IndexViewModel>(p));

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewBag.Users = CreateUsersViewList();

            return View();
        }

        // POST: Projects/Create/:project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel createdProject)
        {
            if (ModelState.IsValid)
            {
                var project = _mapper.Map<Project>(createdProject);
                _projectRepo.Create(project);
                return RedirectToAction(nameof(Index));
            }
            return View(createdProject);
        }

        // GET: Projects/Edit/:id
        public ActionResult Edit(Guid id)
        {
            var project = _projectRepo.GetSingle(id);
            var projectView = _mapper.Map<EditViewModel>(project);
            
            ViewBag.Users = CreateUsersViewList();

            return View(projectView);
        }

        // POST: Projects/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EditViewModel editProject)
        {
            if (id != editProject.Id)
            {
                return BadRequest($"Id {id} does not correspond to {nameof(editProject)} ({id}).");
            }

            if (ModelState.IsValid)
            {
                var project = _mapper.Map<Project>(editProject);
                _projectRepo.Update(id, project);

                return RedirectToAction(nameof(Index));
            }

            return View(editProject);
        }

        // GET: Projects/Delete/:id
        public ActionResult Delete(Guid id)
        {
            var project = _projectRepo.GetSingle(id);
            var projectView = _mapper.Map<IndexViewModel>(project);

            return View(projectView);
        }

        // POST: Projects/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var project = _projectRepo.GetSingle(id);
                _projectRepo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Projects/Details/:id
        public ActionResult Details(Guid id)
        {
            var project = _projectRepo.GetSingle(id);
            var projectView = _mapper.Map<IndexViewModel>(project);

            return View(projectView);
        }

        private List<SelectListItem> CreateUsersViewList()
        {
            return _userRepo.GetAll()
            .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName
                })
            .ToList();
        }
    }
}
