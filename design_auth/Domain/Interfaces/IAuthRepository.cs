using design_web_app.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_web_app.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task GetAuth(Auth auth);
        Task<Auth> ResetPassword(Auth auth);
        Task AddAuth(Auth auth);
    }
}
