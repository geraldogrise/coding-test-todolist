using Azure;
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
           return _context.Posts.Where(x => x.Id_user == id_user).ToList();
        }
    }
}
