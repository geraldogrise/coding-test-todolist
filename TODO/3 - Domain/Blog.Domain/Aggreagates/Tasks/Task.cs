using Todo.Domain.Aggreagates.Core;
using Todo.Domain.Aggreagates.Users;
using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Aggreagates.Tasks
{
    public class Task : EntityCore<Task>
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "O campo Description é obrigatório.")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "O campo Registration Date é obrigatório.")]
        public virtual DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "O campo End Date é obrigatório.")]
        public virtual DateTime? EndDate { get; set; }

        public virtual int Id_user { get; set; }

        public virtual User User { get; set; }
    }
}
