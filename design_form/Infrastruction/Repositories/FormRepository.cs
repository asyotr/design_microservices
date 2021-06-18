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
        public async Task AddForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));
            using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(cmdText: $"INSERT INTO dbo.Form (Login, TZ, Pay, Date) VALUES ('{form.Login}', '{form.TZ}', '{form.Pay}', GETDATE())", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task UpdateStat(Form form)
        {
           using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(cmdText: $"UPDATE dbo.Form SET Status = '{form.Status}' WHERE UID = '{form.UID}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task<Form[]> GetForm(Form form)
        {
            List<FormDTO> getform = new List<FormDTO>();

            using (var connection = new SqlConnection("Server = MSI; Database = Auth; Integrated Security = true"))
            {
                await connection.OpenAsync();
                using var cmd = new SqlCommand(cmdText: $"SELECT * FROM dbo.Form WHERE Login = '{form.Login}'", connection);
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
