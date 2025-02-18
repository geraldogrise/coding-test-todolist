using Todo.Application.ViewModels;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        public UserModel Insert(UserModel user);
        public UserModel Update(int id, UserModel user);
        public bool Delete(int id);
        public UserModel GetById(int id);
        public List<UserModel> GetAll();
        public string Signin(LoginModel loginModel);
    }
}
