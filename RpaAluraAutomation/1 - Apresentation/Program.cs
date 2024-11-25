using RpaAluraAutomation.IoC;
using RpaAluraAutomation.Apresentation.RPA;
using Microsoft.Extensions.DependencyInjection;

namespace RpaAluraAutomation.Apresentation;

class Program
{
    static async Task Main()
    {
        var serviceProvider = DependencyInjectionConfig.Configure();
        var automacao = serviceProvider.GetService<AluraAutomation>();
        if (automacao != null)
        {
            await automacao.BuscarECadastrarTodosCursosAsync("csharp");
            Console.WriteLine("Processo finalizado!");
        }
    }
}
