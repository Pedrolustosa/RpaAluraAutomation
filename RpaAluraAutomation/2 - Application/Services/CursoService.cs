using RpaAluraAutomation.Domain.Entities;
using RpaAluraAutomation.Domain.Interfaces;
using RpaAluraAutomation.Application.Interfaces;

namespace RpaAluraAutomation.Application.Services
{
    public class CursoService(ICursoRepository repository) : ICursoService
    {
        private readonly ICursoRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task InserirCursoAsync(string titulo, string professor, string cargaHoraria, string descricao)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título não pode ser nulo ou vazio.", nameof(titulo));

            if (string.IsNullOrWhiteSpace(professor))
                throw new ArgumentException("O professor não pode ser nulo ou vazio.", nameof(professor));

            if (string.IsNullOrWhiteSpace(cargaHoraria))
                throw new ArgumentException("A carga horária não pode ser nula ou vazia.", nameof(cargaHoraria));

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição não pode ser nula ou vazia.", nameof(descricao));

            try
            {
                var curso = new Curso(titulo, professor, cargaHoraria, descricao);
                await _repository.InserirAsync(curso);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro de validação ao criar o curso: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir o curso no repositório: {ex.Message}");
                throw;
            }
        }
    }
}
