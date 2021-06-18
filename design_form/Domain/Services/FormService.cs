using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using design_form.Domain.Entities;
using design_form.Domain.Interfaces;

namespace design_form.Domain.Services
{
    public class FormService : IFormService 
    {

        private readonly IFormRepository _formRepository;
        public FormService(IFormRepository repository)
        {
            _formRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));
            await _formRepository.AddForm(form);
        }

        public async Task UpdateStat(Form form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));
            await _formRepository.UpdateStat(form);
        }
        public async Task<Form[]> GetForm(Form form)
        {
            return await _formRepository.GetForm(form);
        }
    }
}
