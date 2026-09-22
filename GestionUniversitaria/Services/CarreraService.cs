using GestionUniversitaria.Data;
using GestionUniversitaria.Models;

namespace GestionUniversitaria.Services;

public class CarreraService
{
    private readonly LiteDbContext _db;

    public CarreraService(LiteDbContext db)
    {
        _db = db;
    }

    public string AgregarCarrera(Carrera nuevaCarrera)
    {
        if (string.IsNullOrWhiteSpace(nuevaCarrera.Nombre) ||
            string.IsNullOrWhiteSpace(nuevaCarrera.Facultad) ||
            nuevaCarrera.DuracionAnios <= 0)
        {
            return "Error: Nombre, Facultad y Duración en años son obligatorios. La duración debe ser mayor a 0.";
        }

        if (_db.Carreras.Exists(c => c.Nombre == nuevaCarrera.Nombre && c.Facultad == nuevaCarrera.Facultad))
        {
            return $"Error: Ya existe la carrera {nuevaCarrera.Nombre} en la facultad {nuevaCarrera.Facultad}.";
        }

        _db.Carreras.Insert(nuevaCarrera);

        return $"Éxito: Carrera {nuevaCarrera.Nombre} agregada correctamente con ID {nuevaCarrera.Id}.";
    }

    public List<Carrera> ObtenerTodas()
    {
        return _db.Carreras.FindAll().ToList();
    }

    public Carrera? ObtenerPorId(int id)
    {
        return _db.Carreras.FindById(id);
    }

    public string ActualizarCarrera(Carrera entidadActualizada)
    {
        Carrera? existente = _db.Carreras.FindById(entidadActualizada.Id);
        if (existente is null)
            return $"Error: No existe Carrera con ID {entidadActualizada.Id}.";

        if (string.IsNullOrWhiteSpace(entidadActualizada.Nombre) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Facultad) ||
            entidadActualizada.DuracionAnios <= 0)
        {
            return "Error: Nombre, Facultad y Duración en años son obligatorios. La duración debe ser mayor a 0.";
        }

        if (_db.Carreras.Exists(c => c.Id != entidadActualizada.Id && c.Nombre == entidadActualizada.Nombre && c.Facultad == entidadActualizada.Facultad))
            return $"Error: Ya existe la carrera {entidadActualizada.Nombre} en la facultad {entidadActualizada.Facultad}.";

        _db.Carreras.Update(entidadActualizada);

        return $"Éxito: Carrera con ID {entidadActualizada.Id} actualizada correctamente.";
    }

    public string EliminarCarrera(int id)
    {
        Carrera? existente = _db.Carreras.FindById(id);
        if (existente is null)
            return $"Error: No existe Carrera con ID {id}.";

        int cantidadEstudiantes = _db.Estudiantes.Count(e => e.CarreraId == id);
        int cantidadMaterias = _db.Materias.Count(m => m.CarreraId == id);
        if (cantidadEstudiantes > 0 || cantidadMaterias > 0)
            return $"Error: No se puede eliminar la carrera porque tiene {cantidadEstudiantes} estudiantes y {cantidadMaterias} materias asociadas.";

        _db.Carreras.Delete(id);

        return $"Éxito: Carrera con ID {id} eliminada correctamente.";
    }
}
