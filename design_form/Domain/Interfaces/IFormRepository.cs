using design_form.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_form.Domain.Interfaces
{
    public interface IFormRepository
    {
        Task AddForm(Form form);
        Task UpdateStat(Form form);
        Task<Form[]> GetForm(Form form);
    }
}
