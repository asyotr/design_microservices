using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_web_app.Domain.Entities
{
    public class Auth
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
