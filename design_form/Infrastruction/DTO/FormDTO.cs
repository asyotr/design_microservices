using design_form.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_form.Infrastruction.DTO
{
    public class FormDTO
    {
        public string Login { get; set; }
        public string UID { get; set; }
        public string TZ { get; set; }
        public string Status { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }

        public Form ToModel()
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
