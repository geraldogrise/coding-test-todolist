using Todo.Domain.Aggreagates.Users;
using Todo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Aggreagates.Tasks.Repository
{

    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetByUser(int id_user);
        List<TaskUser> GetTasks();
    }
}
