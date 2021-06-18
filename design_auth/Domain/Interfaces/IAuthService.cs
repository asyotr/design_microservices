using design_web_app.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace design_web_app.Domain.Interfaces
{
    /// <summary>
    /// Работа с интерфейсом
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Я пока хз, что возвращает, но предполагаю логин-пароль
        /// </summary>
        /// <returns></returns>
        Task GetAuth(Auth auth);
        Task<Auth> ResetPassword(Auth auth);
        Task AddAuth(Auth auth);
    }
}
