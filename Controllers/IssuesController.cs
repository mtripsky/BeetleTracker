using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BeetleTracker.Data;
using BeetleTracker.Models.Entities;
using BeetleTracker.Models.IssueViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BeetleTracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IEntityBaseRepository<Issue> _issueRepo;
        private readonly IEntityBaseRepository<ApplicationUser> _userRepo;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 10;  // should be in constructor
        private int _pageIndex;  // should be in constructor

        public IssuesController(
            IEntityBaseRepository<Issue> issueRepo,
            IEntityBaseRepository<ApplicationUser> userRepo,
            IMapper mapper)
        {
            _issueRepo = issueRepo;
            _userRepo = userRepo;
            _mapper = mapper;
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

            var issues = _issueRepo
                .GetAll()
                .Select(i => _mapper.Map<IndexViewModel>(i));

            issues = sortBy switch
            {
                "Updated desc" => issues.OrderByDescending(x => x.Updated),
                "Updated" => issues.OrderBy(x => x.Updated),
                "Reporter desc" => issues.OrderByDescending(x => x.Reporter),
                "Priority" => issues.OrderBy(x => x.Priority),
                "Priority desc" => issues.OrderByDescending(x => x.Priority),
                "Reporter" => issues.OrderBy(x => x.Reporter),
                "Status desc" => issues.OrderByDescending(x => x.Status),
                "Status" => issues.OrderBy(x => x.Status),
                "Created desc" => issues.OrderByDescending(x => x.Created),
                _ => issues.OrderBy(x => x.Created),
            };
            return View(issues.ToPagedList(_pageIndex, _pageSize));
        }

        // GET: Issues/Create
        public IActionResult Create()
        {            
            ViewBag.Users = CreateUsersViewList();

            return View();
        }

        // POST: Issues/Create/:project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel createdIssue)
        {
            var timeNow = DateTime.Now;
            createdIssue.Created = timeNow;
            createdIssue.Updated = timeNow;

            ViewBag.Users = CreateUsersViewList();

            if (ModelState.IsValid)
            {
                var issue = _mapper.Map<Issue>(createdIssue);
                _issueRepo.Create(issue);
                return RedirectToAction(nameof(Index));
            }

            return View(createdIssue);
        }

        // GET: Issues/Edit/:id
        public ActionResult Edit(Guid id)
        {
            ViewBag.Users = CreateUsersViewList();
            var issue = _issueRepo.GetSingle(id);
            var issueView = _mapper.Map<EditViewModel>(issue);

            return View(issueView);
        }

        // POST: Issues/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EditViewModel editIssue)
        {
            if (id != editIssue.Id)
            {
                return BadRequest($"Id {id} does not correspond to {nameof(editIssue)} ({id}).");
            }

            if (ModelState.IsValid)
            {
                var oldIssue = _issueRepo.GetSingle(id);
                editIssue.Created = oldIssue.Created;

                var issue = _mapper.Map<Issue>(editIssue);
                issue.Updated = DateTime.Now;

                _issueRepo.Update(id, issue);
                return RedirectToAction(nameof(Index));
            }

            return View(editIssue);
        }

        // GET: Issues/Delete/:id
        public ActionResult Delete(Guid id)
        {
            var issue = _issueRepo.GetSingle(id);
            var issueView = _mapper.Map<IndexViewModel>(issue);

            return View(issueView);
        }

        // POST: Issues/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var issue = _issueRepo.GetSingle(id);
                _issueRepo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Issues/Details/:id
        public ActionResult Details(Guid id)
        {
            var issue = _issueRepo.GetSingle(id);
            var issueView = _mapper.Map<DetailViewModel>(issue);

            return View(issueView);
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
