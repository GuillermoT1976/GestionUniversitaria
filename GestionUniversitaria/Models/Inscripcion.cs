namespace GestionUniversitaria.Models;

public class Inscripcion
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }
    public Estudiante Estudiante { get; set; } = null!;

    public int MateriaId { get; set; }
    public Materia Materia { get; set; } = null!;

    public int ProfesorId { get; set; }
    public Profesor Profesor { get; set; } = null!;

    public DateTime FechaInscripcion { get; set; }
    public decimal? Nota { get; set; }
    public EstadoInscripcion Estado { get; set; }

    public List<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
}
