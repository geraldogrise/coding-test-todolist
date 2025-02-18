using AutoMapper;
using Todo.Application.ViewModels;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Application.AutoMapper
{

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<Domain.Aggreagates.Tasks.Task, TaskModel>();
            CreateMap<TaskUser, TaskUserModel>();
            CreateMap<Login, LoginModel>();
        }
    }
}
