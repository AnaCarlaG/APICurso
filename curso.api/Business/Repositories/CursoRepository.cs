using curso.api.Business.Entities;
using curso.api.Business.Repositories.Interfaces;
using curso.api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _context;

        public CursoRepository(CursoDbContext context)
        {
            this._context = context;
        }

        public void Add(Curso curso)
        {
            _context.Curso.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Curso> ObterCursoPorUsuario(int codigoUsuario)
        {
           return _context.Curso.Include(i=>i.Usuario).Where(c => c.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
