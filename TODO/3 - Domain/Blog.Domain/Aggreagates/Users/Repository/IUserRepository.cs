using Todo.Domain.Interfaces;


namespace Todo.Domain.Aggreagates.Users.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public User Signin(Login login);
    }
}
