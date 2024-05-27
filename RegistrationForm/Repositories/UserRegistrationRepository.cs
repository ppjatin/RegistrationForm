using RegistrationForm.Data;
using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RegistrationForm.Repositories
{
    public class UserRegistrationRepository: IUserRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserRegistration> GetUserRegistrationById(int id)
        {
            IList<UserRegistration> users = new List<UserRegistration>();
            using (var connection = _context.Database.Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetUserRegistration";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", id));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserRegistration
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Address = reader["Address"].ToString(),
                                StateName = reader["StateName"].ToString(),
                                CityName = reader["CityName"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return users;
        }


        public IList<State> GetAllSate()
        {
            var statelist = new List<State>();
            using (var connection = _context.Database.Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAllState";
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var state = new State
                            {
                                Id = (int)reader["Id"],
                                StateName = reader["StateName"].ToString(),
                            };
                            statelist.Add(state);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return statelist;
        }

        public void InsertUserRegistration(INUserRegistration user)
        {

            using (var connection = _context.Database.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("InsertUserRegistration", (SqlConnection)connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@StateId", user.StateId);
                    cmd.Parameters.AddWithValue("@CityId", user.CityId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public IList<City> GetAllCity(int id)
        {
            var citylist = new List<City>();
            using (var connection = _context.Database.Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = "getcities";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@stateid", id));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cty = new City
                            {
                                Id = (int)reader["Id"],
                                CityName = reader["CityName"].ToString(),
                            };
                            citylist.Add(cty);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return citylist;
        }

        public void UpdateUserRegistration(INUserRegistration user)
        {
            using (var connection = _context.Database.Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateUserRegistration";
                command.CommandType = CommandType.StoredProcedure;

               
                command.Parameters.Add(new SqlParameter("@Id", user.Id));
                command.Parameters.Add(new SqlParameter("@Name", user.Name));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@Phone", user.Phone));
                command.Parameters.Add(new SqlParameter("@Address", user.Address));
                command.Parameters.Add(new SqlParameter("@StateId", user.StateId));
                command.Parameters.Add(new SqlParameter("@CityId", user.CityId));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteUserRegistration(int id)
        {
            using (var connection = _context.Database.Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteUserRegistration";
                command.CommandType = CommandType.StoredProcedure;

               
                command.Parameters.Add(new SqlParameter("@Id", id));
                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }




    }
}