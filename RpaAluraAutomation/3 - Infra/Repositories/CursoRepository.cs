using RpaAluraAutomation.Infra.Context;
using RpaAluraAutomation.Domain.Entities;
using RpaAluraAutomation.Domain.Interfaces;

namespace RpaAluraAutomation.Infra.Repositories;

public class CursoRepository(CursoContext context) : ICursoRepository
{
    private readonly CursoContext _context = context;

    public async Task InserirAsync(Curso curso)
    {
        try
        {
            if (curso != null)
            {
                await _context.Cursos.AddAsync(curso);
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"Número de registros salvos no banco: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar curso no banco: {ex.Message}");
        }
    }
}