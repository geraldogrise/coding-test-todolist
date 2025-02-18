using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Aggreagates.Tasks
{
    public class TaskUser
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
