using Azure;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Data.Core.Repository;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Aggreagates.Tasks.Repository;

namespace Todo.Data.Repository
{
    public class TaskRepository : Repository<Domain.Aggreagates.Tasks.Task>, ITaskRepository
    {
        public TaskRepository(DatabaseContext context) : base(context)
        {

        }

        public List<Domain.Aggreagates.Tasks.Task> GetByUser(int id_user)
        {
           return _context.Tasks.Where(x => x.Id_user == id_user).ToList();
        }

        public List<TaskUser> GetTasks()
        {
            return _context.Tasks
            .Include(t => t.User)
            .Select(t => new TaskUser
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                RegistrationDate = t.RegistrationDate,
                EndDate = t.EndDate,
                Login = t.User.Login,
                User = t.User.Name,
                Email = t.User.Email
            })
            .ToList();
        }
    }
}
