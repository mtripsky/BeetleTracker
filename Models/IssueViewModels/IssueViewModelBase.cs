using System;
using System.ComponentModel.DataAnnotations;
using BeetleTracker.Models.Entities;

namespace BeetleTracker.Models.IssueViewModels
{
    public abstract class IssueViewModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Reporter { get; set; } 

        public IssuePriority Priority { get; set; }

        public IssueStatus Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Created { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Updated { get; set; }
    }
}