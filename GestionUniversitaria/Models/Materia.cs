namespace GestionUniversitaria.Models;

public class Materia
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int Creditos { get; set; }

    public int CarreraId { get; set; }
    public Carrera Carrera { get; set; } = null!;

    public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
}
