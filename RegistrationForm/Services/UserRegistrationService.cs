using RegistrationForm.Models;
using RegistrationForm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationForm.Services
{
    public class UserRegistrationService
    {
        private readonly IUserRegistrationRepository _repository;

        public UserRegistrationService(IUserRegistrationRepository repository)
        {
            _repository = repository;
        }

        public IList<UserRegistration> GetUserRegistrationById(int id)
        {
            return _repository.GetUserRegistrationById(id);
        }

        public IList<State> GetAllSate() { return _repository.GetAllSate(); }

        public IList<City> GetAllCity(int id) { return _repository.GetAllCity(id); }

        public void InsertUserRegistration(INUserRegistration user) {  _repository.InsertUserRegistration(user); }

        public void UpdateUserRegistration(INUserRegistration user) { _repository.UpdateUserRegistration(user); }

        public void DeleteUserRegistration(int id) { _repository.DeleteUserRegistration(id); }

    }
}