using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using design_web_app.Domain.Entities;
using design_web_app.Domain.Interfaces;

namespace design_web_app.Domain.Services
{
    public class AuthService : IAuthService 
    {

        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository repository)
        {
            _authRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task GetAuth(Auth auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));
            await _authRepository.GetAuth(auth);
        }

        public async Task<Auth> ResetPassword(Auth auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));
            return await _authRepository.ResetPassword(auth);
        }

        public async Task AddAuth(Auth auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));
            await _authRepository.AddAuth(auth);
        }
    }
}
