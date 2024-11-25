using RpaAluraAutomation.Domain.Entities;

namespace RpaAluraAutomation.Domain.Interfaces;

public interface ICursoRepository
{
    Task InserirAsync(Curso curso);
}
