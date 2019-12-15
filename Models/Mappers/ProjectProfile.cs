using System;
using BeetleTracker.Models.ProjectViewModels;
using BeetleTracker.Models.Entities;
using AutoMapper;
using BeetleTracker.Data;
using System.Linq;

namespace BeetleTracker.Models.Mappers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile(IEntityBaseRepository<ApplicationUser> userRepo)
        {
            CreateMap<Project, ProjectViewModelBase>()
                .ForMember(dest => dest.ProjectLead, opt => opt.MapFrom(src => userRepo.GetSingle(src.ProjectLeadId).UserName));
            CreateMap<Project, IndexViewModel>()
                .IncludeBase<Project, ProjectViewModelBase>();  
            CreateMap<Project, EditViewModel>()
                .IncludeBase<Project, ProjectViewModelBase>(); 
            CreateMap<Project, CreateViewModel>()
                .IncludeBase<Project, ProjectViewModelBase>(); 

            CreateMap<ProjectViewModelBase, Project>()
                .ForMember(dest => dest.ProjectLeadId, opt => opt.MapFrom(src => userRepo.GetSingle(new Guid(src.ProjectLead)).Id));
            CreateMap<CreateViewModel, Project>()
                .IncludeBase<ProjectViewModelBase, Project>();              
            CreateMap<EditViewModel, Project>()
                .IncludeBase<ProjectViewModelBase, Project>(); 
        }
    }
}
