using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RpaAluraAutomation.Infra.Context;

public class CursoContextFactory : IDesignTimeDbContextFactory<CursoContext>
{
    public CursoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CursoContext>();
        optionsBuilder.UseSqlite(@"Data Source=C:\Developer\RpaAluraAutomation\RpaAluraAutomation\Cursos.db");

        return new CursoContext(optionsBuilder.Options);
    }
}