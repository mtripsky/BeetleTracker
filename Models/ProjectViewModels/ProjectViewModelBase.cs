using System;
using BeetleTracker.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeetleTracker.Models.ProjectViewModels
{
    public class ProjectViewModelBase
    {  
        public Guid Id {get; set;}

        public string Name { get; set; }

        [Display(Name = "Project Lead")]
        public string ProjectLead { get; set; }

        public Category Category { get; set; }

        public string Description {get; set;}

        public string Url { get; set; }
    }
}