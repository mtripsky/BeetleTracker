using BeetleTracker.Models.Entities;

namespace BeetleTracker.Models.IssueViewModels
{
    public class CreateViewModel : IssueViewModelBase
    {
        public string Assignee {get ;set;}

        public IssueType Type {get; set;}

        public string Description {get; set; }
    }
}