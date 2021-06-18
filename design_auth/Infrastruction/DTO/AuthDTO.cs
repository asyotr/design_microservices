using design_web_app.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_web_app.Infrastruction.DTO
{
    public class AuthDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }



        public Auth ToModel()
        {
            return new Auth()
            {
                Login = this.Login,
                Password = this.Password,
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email
            };
        }
    }
}
