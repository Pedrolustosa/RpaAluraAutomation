using Microsoft.EntityFrameworkCore;
using RpaAluraAutomation.Domain.Entities;

namespace RpaAluraAutomation.Infra.Context;

public class CursoContext(DbContextOptions<CursoContext> options) : DbContext(options)
{
    public DbSet<Curso> Cursos { get; set; }
}
