namespace RpaAluraAutomation.Domain.Entities;

public class Curso
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Professor { get; private set; }
    public string CargaHoraria { get; private set; }
    public string Descricao { get; private set; }

    private Curso() { }

    public Curso(string titulo, string professor, string cargaHoraria, string descricao)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(professor)) throw new ArgumentException("Professor é obrigatório.", nameof(professor));
        if (string.IsNullOrWhiteSpace(cargaHoraria)) throw new ArgumentException("Carga horária é obrigatória.", nameof(cargaHoraria));
        if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        Titulo = titulo;
        Professor = professor;
        CargaHoraria = cargaHoraria;
        Descricao = descricao;
    }
}
