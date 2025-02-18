
using Todo.Application.ViewModels;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Application.Interfaces
{
    public interface ITaskAppService : IDisposable
    {
        public TaskModel Insert(TaskModel task);
        public TaskModel Update(int id, TaskModel task);
        public bool Delete(int id);
        public TaskModel GetById(int id);
        public List<TaskModel> GetByUser(int id_user);
        public List<TaskUserModel> GetTasks();
        public List<TaskModel> GetAll();
    }
}
