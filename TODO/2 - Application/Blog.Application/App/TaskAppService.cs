using AutoMapper;
using Todo.Application.App.Core;
using Todo.Application.Interfaces;
using Todo.Application.ViewModels;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Aggreagates.Tasks.Services;
using Todo.Domain.Notifications;
using MediatR;

namespace Todo.Application.App
{
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        private readonly IMapper _mapper;

        private readonly ITaskService _postService;

        public TaskAppService(IMediator mediator,
                              INotificationHandler<DomainNotification> notifications,
                              IMapper mapper,
                              ITaskService postService
                                      ) : base(mediator, notifications)
        {
            _mapper = mapper;
            _postService = postService;
        }


        public TaskModel Insert(TaskModel post)
        {
            var response = _postService.Insert(_mapper.Map<Domain.Aggreagates.Tasks.Task>(post));
            return _mapper.Map<TaskModel>(response);
        }

        public TaskModel Update(int id, TaskModel post)
        {
            var response = _postService.Update(id, _mapper.Map<Domain.Aggreagates.Tasks.Task>(post));
            return _mapper.Map<TaskModel>(response);
        }

        public bool Delete(int id)
        {
            return _postService.Delete(id);
        }

        public TaskModel GetById(int id)
        {
            return _mapper.Map<TaskModel>(_postService.GetById(id));
        }

        public List<TaskModel> GetByUser(int id_user)
        {
            return _mapper.Map<List<TaskModel>>(_postService.GetByUser(id_user));
        }

        public List<TaskModel> GetAll()
        {
            return _mapper.Map<List<TaskModel>>(_postService.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _postService.Dispose();
        }

    }
}
