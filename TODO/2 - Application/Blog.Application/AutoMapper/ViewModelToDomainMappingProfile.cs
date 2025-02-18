using AutoMapper;
using Todo.Application.ViewModels;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<TaskModel, Domain.Aggreagates.Tasks.Task>();
            CreateMap<LoginModel, Login>();
        }
    }
}
