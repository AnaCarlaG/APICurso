using curso.api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories.Interfaces
{
    public interface ICursoRepository
    {
        void Add(Curso curso);
        void Commit();
        IList<Curso> ObterCursoPorUsuario(int codigoUsuario);
    }
}
