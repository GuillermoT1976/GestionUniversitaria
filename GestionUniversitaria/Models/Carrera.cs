namespace GestionUniversitaria.Models;

public class Carrera
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Facultad { get; set; } = string.Empty;
    public int DuracionAnios { get; set; }

    public List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    public List<Materia> Materias { get; set; } = new List<Materia>();
}
