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
        public async Task AddAuth(Auth auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));
            using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(cmdText: $"INSERT INTO dbo.Auth (Login, Password, Name, Surname, Email, Date) VALUES ('{auth.Login}', '{auth.Password}', '{auth.Name}', '{auth.Surname}', '{auth.Email}', GETDATE())", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task GetAuth(Auth auth)
        {
            using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(cmdText: $"SELECT COUNT(*) AS count FROM dbo.Auth WHERE Login = '{auth.Login}' AND Password = '{auth.Password}'", connection))
                {
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

            using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(cmdText: $"UPDATE dbo.Auth SET Password = '{password}' WHERE Login = '{auth.Login}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            auth.Password = password;
            return (auth);
        }
    }
}
