namespace GestionUniversitaria.Models;

public class Estudiante
{
    public int Id { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public bool Activo { get; set; }

    public int CarreraId { get; set; }
    public Carrera Carrera { get; set; } = null!;

    public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
}
