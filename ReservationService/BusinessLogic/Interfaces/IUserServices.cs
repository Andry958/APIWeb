using BusinessLogic.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserServices
    {
        public Task Register(UserRegisterModel model);
        public Task Login(UserLoginModel model);
        public Task Logout(LogoutModel model);
        public Task<List<UserGetModel>> GetAll();
    }
}
