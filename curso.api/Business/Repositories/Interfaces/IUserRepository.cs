using curso.api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add(User usuario);
        void Commit();
        User ObterUsuario(string login);
    }
}
