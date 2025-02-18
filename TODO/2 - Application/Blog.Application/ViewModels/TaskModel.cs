using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.ViewModels
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int Id_user { get; set; }

    }
}
