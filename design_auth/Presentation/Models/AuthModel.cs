using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using design_web_app.Domain.Entities;

namespace design_web_app.Presentation.Models
{
    public class AuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public AuthModel()
        {

        }

        public AuthModel(Auth auth)
        {
            Login = auth?.Login ?? throw new ArgumentNullException(nameof(auth.Login));
            Password = auth?.Password ?? throw new ArgumentNullException(nameof(auth.Password));
            Name = auth?.Name ?? throw new ArgumentNullException(nameof(auth.Name));
            Surname = auth?.Surname ?? throw new ArgumentNullException(nameof(auth.Surname));
            Email = auth?.Email ?? throw new ArgumentNullException(nameof(auth.Email));
        }

        public Auth ToEntity()
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
