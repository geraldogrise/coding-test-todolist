using Todo.Domain.Aggreagates.Core;
using Todo.Domain.Aggreagates.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Todo.Domain.Aggreagates.Users
{
    public class User : EntityCore<User>
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public virtual string Password { get; set; }

        public virtual List<Domain.Aggreagates.Tasks.Task> TaskList { get; set; }

    }
}

