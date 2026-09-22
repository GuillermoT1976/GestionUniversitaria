using GestionUniversitaria.Data;
using GestionUniversitaria.Models;

namespace GestionUniversitaria.Services;

public class ProfesorService
{
    private readonly LiteDbContext _db;

    public ProfesorService(LiteDbContext db)
    {
        _db = db;
    }

    public string AgregarProfesor(Profesor nuevoProfesor)
    {
        if (string.IsNullOrWhiteSpace(nuevoProfesor.Nombre) ||
            string.IsNullOrWhiteSpace(nuevoProfesor.Apellido) ||
            string.IsNullOrWhiteSpace(nuevoProfesor.Email) ||
            string.IsNullOrWhiteSpace(nuevoProfesor.Especialidad))
        {
            return "Error: Nombre, Apellido, Email y Especialidad son obligatorios.";
        }

        if (_db.Profesores.Exists(p => p.Email == nuevoProfesor.Email))
        {
            return $"Error: El email {nuevoProfesor.Email} ya está registrado para otro profesor.";
        }

        _db.Profesores.Insert(nuevoProfesor);

        return $"Éxito: Profesor {nuevoProfesor.Nombre} {nuevoProfesor.Apellido} agregado correctamente con ID {nuevoProfesor.Id}.";
    }

    public List<Profesor> ObtenerTodos()
    {
        return _db.Profesores.FindAll().ToList();
    }

    public Profesor? ObtenerPorId(int id)
    {
        return _db.Profesores.FindById(id);
    }

    public string ActualizarProfesor(Profesor entidadActualizada)
    {
        Profesor? existente = _db.Profesores.FindById(entidadActualizada.Id);
        if (existente is null)
            return $"Error: No existe Profesor con ID {entidadActualizada.Id}.";

        if (string.IsNullOrWhiteSpace(entidadActualizada.Nombre) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Apellido) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Email) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Especialidad))
        {
            return "Error: Nombre, Apellido, Email y Especialidad son obligatorios.";
        }

        if (_db.Profesores.Exists(p => p.Id != entidadActualizada.Id && p.Email == entidadActualizada.Email))
            return $"Error: El email {entidadActualizada.Email} ya está registrado para otro profesor.";

        _db.Profesores.Update(entidadActualizada);

        return $"Éxito: Profesor con ID {entidadActualizada.Id} actualizado correctamente.";
    }

    public string EliminarProfesor(int id)
    {
        Profesor? existente = _db.Profesores.FindById(id);
        if (existente is null)
            return $"Error: No existe Profesor con ID {id}.";

        if (_db.Inscripciones.Exists(i => i.ProfesorId == id))
            return "Error: No se puede eliminar el profesor porque tiene inscripciones registradas.";

        _db.Profesores.Delete(id);

        return $"Éxito: Profesor con ID {id} eliminado correctamente.";
    }
}
