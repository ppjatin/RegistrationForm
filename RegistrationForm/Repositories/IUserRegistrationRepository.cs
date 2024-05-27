using RegistrationForm.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RegistrationForm.Repositories
{
    public interface IUserRegistrationRepository
    {
         IList<UserRegistration> GetUserRegistrationById(int id);
         IList<State> GetAllSate();

        IList<City> GetAllCity(int id);

        void InsertUserRegistration(INUserRegistration user);
        void UpdateUserRegistration(INUserRegistration user);

        void DeleteUserRegistration(int id);


    }
}
