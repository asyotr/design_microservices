using design_web_app.Domain.Entities;
using design_web_app.Domain.Interfaces;
using design_web_app.Infrastruction.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace design_web_app.Infrastruction.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private const string ConnectionString = "Server = MSI; Database = Auth; Integrated Security = true";
        public async Task AddAuth(Auth auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"INSERT INTO dbo.Auth (Login, Password, Name, Surname, Email, Date) VALUES (@login, @password, @name, @surname, @email, GETDATE())";
               
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("login", auth.Login);
                    cmd.Parameters.AddWithValue("password", auth.Password);
                    cmd.Parameters.AddWithValue("name", auth.Name);
                    cmd.Parameters.AddWithValue("surname", auth.Surname);
                    cmd.Parameters.AddWithValue("email", auth.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task GetAuth(Auth auth)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"SELECT COUNT(*) AS count FROM dbo.Auth WHERE Login = @login AND Password = @password";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("login", auth.Login);
                    cmd.Parameters.AddWithValue("password", auth.Password);

                    var reader = await cmd.ExecuteReaderAsync();
                    reader.Read();
                    int count = int.Parse(reader["count"].ToString());
                    if (count != 1)
                        throw new ArgumentNullException(nameof(auth));

                }
            }
        }
        
        private static Random random = new Random();

        public async Task<Auth> ResetPassword(Auth auth)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string password = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"UPDATE dbo.Auth SET Password = @password WHERE Login = @login";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("login", auth.Login);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.ExecuteNonQuery();
                }
            }
            auth.Password = password;
            return (auth);
        }
    }
}
