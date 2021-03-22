using curso.api.Business.Entities;
using curso.api.Business.Repositories.Interfaces;
using curso.api.Infraestruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CursoDbContext _context;

        public UserRepository(CursoDbContext context)
        {
            this._context = context;
        }

        public void Add(User usuario)
        {
            _context.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public User ObterUsuario(string login )
        {
            return _context.User.FirstOrDefault(user => user.Login == login);

        }
    }
}
