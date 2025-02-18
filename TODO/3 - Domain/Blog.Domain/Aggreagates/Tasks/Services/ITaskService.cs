
namespace Todo.Domain.Aggreagates.Tasks.Services
{
    public interface ITaskService : IDisposable
    {
        public Task Insert(Task task);

        public Task Update(int id, Task task);

        public bool Delete(int id);

        public Task GetById(int id);

        public List<Task> GetAll();

        public List<Task> GetByUser(int id_user);

        public List<TaskUser> GetTasks();
    }
}
