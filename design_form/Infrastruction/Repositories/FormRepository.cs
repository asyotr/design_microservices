using design_form.Domain.Entities;
using design_form.Domain.Interfaces;
using design_form.Infrastruction.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace design_form.Infrastruction.Repositories
{
    public class FormRepository : IFormRepository
    {
        private const string ConnectionString = "Server = MSI; Database = Auth; Integrated Security = true";
        public async Task AddForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"INSERT INTO dbo.Form (Login, TZ, Pay, Date) VALUES (@login, @tz, @pay, GETDATE())";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("login", form.Login);
                    cmd.Parameters.AddWithValue("tz", form.TZ);
                    cmd.Parameters.AddWithValue("pay", form.Pay);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task UpdateStat(Form form)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"UPDATE dbo.Form SET Status = @status WHERE UID = @uid";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("status", form.Status);
                    cmd.Parameters.AddWithValue("uid", form.UID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task<Form[]> GetForm(Form form)
        {
            List<FormDTO> getform = new List<FormDTO>();
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = $"SELECT * FROM dbo.Form WHERE Login = @login";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@login", form.Login);
                var reader = await cmd.ExecuteReaderAsync();


                while (reader.Read())
                {
                    getform.Add(new FormDTO()
                    {
                        Login = reader["Login"].ToString(),
                        TZ = reader["TZ"].ToString(),
                        Pay = reader["Pay"].ToString(),
                        Status = reader["Status"].ToString(),
                        Date = reader["Date"].ToString(),
                        UID = reader["UID"].ToString(),
                    });

                }
            } 

            return getform.Select(e => e.ToEntity()).ToArray();
        }


    }
}
