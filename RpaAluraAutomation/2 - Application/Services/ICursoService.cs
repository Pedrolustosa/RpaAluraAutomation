namespace RpaAluraAutomation.Application.Interfaces;

public interface ICursoService
{
    Task InserirCursoAsync(string titulo, string professor, string cargaHoraria, string descricao);
}
