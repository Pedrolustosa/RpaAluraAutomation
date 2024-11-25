using Microsoft.EntityFrameworkCore;
using RpaAluraAutomation.Infra.Context;
using RpaAluraAutomation.Domain.Interfaces;
using RpaAluraAutomation.Apresentation.RPA;
using RpaAluraAutomation.Infra.Repositories;
using RpaAluraAutomation.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using RpaAluraAutomation.Application.Interfaces;

namespace RpaAluraAutomation.IoC;

public static class DependencyInjectionConfig
{
    public static ServiceProvider Configure()
    {
        try
        {
            var services = new ServiceCollection();
            services.AddDbContext<CursoContext>(options =>
                options.UseSqlite(@"Data Source=C:\Developer\RpaAluraAutomation\RpaAluraAutomation\Cursos.db"));
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddSingleton<AluraAutomation>();
            return services.BuildServiceProvider();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao configurar as dependências: {ex.Message}");
            throw;
        }
    }
}