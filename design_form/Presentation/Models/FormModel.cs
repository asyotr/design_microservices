using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using design_form.Domain.Entities;

namespace design_form.Presentation.Models
{
    public class FormModel
    {
        public string Login { get; set; }
        public string UID { get; set; }
        public string TZ { get; set; }
        public string Status { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }

        public FormModel()
        {
            Date = DateTime.Now.ToString();
        }

        public FormModel(Form form)
        {
            Login = form?.Login ?? "";
            UID = form?.UID ?? "";
            TZ = form?.TZ ?? "";
            Pay = form?.Pay ?? "";
            Date = form?.Date ?? "";
            Status = form?.Status ?? "";
        }

        public Form ToEntity()
        {
            return new Form()
            {
                Login = this.Login,
                UID = this.UID,
                TZ = this.TZ,
                Status = this.Status,
                Pay = this.Pay,
                Date = this.Date
            };
        }
    }
}
