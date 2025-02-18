using Todo.Domain.Aggreagates.Tasks.Repository;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Aggreagates.Tasks.Services;
using Todo.Domain.Services;
using MediatR;
using Todo.Domain.Aggreagates.Users;


namespace Todo.Domain.Aggreagates.Tasks.Services
{
    public class TasltService : Service, ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TasltService(IMediator mediator,
                             ITaskRepository taskRepository
                             ) : base(mediator)
        {
            _taskRepository = taskRepository;
        }

        public Task Insert(Task task)
        {
            _taskRepository.Add(task);
            _taskRepository.SaveChanges();
            return task;
        }


        public Task Update(int id,Task task)
        {
            task.Id = id;
            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
            return task;
        }

        public bool Delete(int id)
        {
            _taskRepository.Remove(GetById(id));
            _taskRepository.SaveChanges();
            return true;
        }

        public Task GetById(int id)
        {
            return _taskRepository.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Task> GetByUser(int id_user)
        {
            return _taskRepository.GetByUser(id_user).ToList();
        }

        public List<TaskUser> GetTasks()
        {
            return _taskRepository.GetTasks();
        }

        public List<Task> GetAll()
        {
            return _taskRepository.GetAll().ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _taskRepository.Dispose();
        }

    }
}
