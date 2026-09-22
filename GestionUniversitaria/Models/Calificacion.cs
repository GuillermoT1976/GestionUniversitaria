namespace GestionUniversitaria.Models;

public class Calificacion
{
    public int Id { get; set; }

    public int InscripcionId { get; set; }
    public Inscripcion Inscripcion { get; set; } = null!;

    public int Parcial { get; set; }
    public decimal Nota { get; set; }
}
