using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.ViewModels
{
    public class TaskUserModel
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime RegistrationDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public virtual string Login { get; set; }
        public virtual string User { get; set; }
        public virtual string Email { get; set; }
    }
}
