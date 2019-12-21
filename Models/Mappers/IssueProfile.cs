using System;
using BeetleTracker.Models.IssueViewModels;
using BeetleTracker.Models.Entities;
using AutoMapper;
using BeetleTracker.Data;
using System.Linq;

namespace BeetleTracker.Models.Mappers
{
    public class IssueProfile : Profile
    {
        public IssueProfile(IEntityBaseRepository<ApplicationUser> userRepo)
        {
            CreateMap<Issue, IssueViewModelBase>()
                .ForMember(dest => dest.Reporter, opt => opt.MapFrom(src => userRepo.GetSingle(src.ReporterId).UserName));
            CreateMap<Issue, IndexViewModel>()
                .IncludeBase<Issue, IssueViewModelBase>();  
            CreateMap<Issue, EditViewModel>()
                .IncludeBase<Issue, IssueViewModelBase>(); 
            CreateMap<Issue, CreateViewModel>()
                .IncludeBase<Issue, IssueViewModelBase>(); 
            CreateMap<Issue, DetailViewModel>()
                .IncludeBase<Issue, IssueViewModelBase>(); 

            CreateMap<IssueViewModelBase, Issue>()
                .ForMember(dest => dest.ReporterId, opt => opt.MapFrom(src => userRepo.GetSingle(new Guid(src.Reporter)).Id));
            CreateMap<CreateViewModel, Issue>()
                .IncludeBase<IssueViewModelBase, Issue>();              
            CreateMap<EditViewModel, Issue>()
                .IncludeBase<IssueViewModelBase, Issue>(); 
        }
    }
}
