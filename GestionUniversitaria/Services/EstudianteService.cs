using GestionUniversitaria.Data;
using GestionUniversitaria.Models;

namespace GestionUniversitaria.Services;

public class EstudianteService
{
    private readonly LiteDbContext _db;

    public EstudianteService(LiteDbContext db)
    {
        _db = db;
    }

    public string AgregarEstudiante(Estudiante nuevoEstudiante)
    {
        if (string.IsNullOrWhiteSpace(nuevoEstudiante.Nombre) ||
            string.IsNullOrWhiteSpace(nuevoEstudiante.Apellido) ||
            string.IsNullOrWhiteSpace(nuevoEstudiante.Matricula))
        {
            return "Error: Nombre, Apellido y Matrícula son obligatorios.";
        }

        if (_db.Estudiantes.Exists(e => e.Matricula == nuevoEstudiante.Matricula))
        {
            return $"Error: Ya existe un estudiante con la matrícula {nuevoEstudiante.Matricula}.";
        }

        if (_db.Estudiantes.Exists(e => e.Email == nuevoEstudiante.Email))
        {
            return $"Error: El email {nuevoEstudiante.Email} ya está registrado.";
        }

        if (_db.Carreras.FindById(nuevoEstudiante.CarreraId) is null)
        {
            return $"Error: La carrera con ID {nuevoEstudiante.CarreraId} no existe.";
        }

        nuevoEstudiante.Activo = true;

        _db.Estudiantes.Insert(nuevoEstudiante);

        return $"Éxito: Estudiante {nuevoEstudiante.Nombre} {nuevoEstudiante.Apellido} agregado correctamente con ID {nuevoEstudiante.Id}.";
    }

    public List<Estudiante> ObtenerTodos()
    {
        return _db.Estudiantes.FindAll().ToList();
    }

    public Estudiante? ObtenerPorId(int id)
    {
        return _db.Estudiantes.FindById(id);
    }

    public string ActualizarEstudiante(Estudiante entidadActualizada)
    {
        Estudiante? existente = _db.Estudiantes.FindById(entidadActualizada.Id);
        if (existente is null)
            return $"Error: No existe Estudiante con ID {entidadActualizada.Id}.";

        if (string.IsNullOrWhiteSpace(entidadActualizada.Nombre) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Apellido) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Matricula))
        {
            return "Error: Nombre, Apellido y Matrícula son obligatorios.";
        }

        if (_db.Estudiantes.Exists(e => e.Id != entidadActualizada.Id && e.Matricula == entidadActualizada.Matricula))
            return $"Error: Ya existe un estudiante con la matrícula {entidadActualizada.Matricula}.";

        if (_db.Estudiantes.Exists(e => e.Id != entidadActualizada.Id && e.Email == entidadActualizada.Email))
            return $"Error: El email {entidadActualizada.Email} ya está registrado.";

        if (_db.Carreras.FindById(entidadActualizada.CarreraId) is null)
            return $"Error: La carrera con ID {entidadActualizada.CarreraId} no existe.";

        _db.Estudiantes.Update(entidadActualizada);

        return $"Éxito: Estudiante con ID {entidadActualizada.Id} actualizado correctamente.";
    }

    public string EliminarEstudiante(int id)
    {
        Estudiante? existente = _db.Estudiantes.FindById(id);
        if (existente is null)
            return $"Error: No existe Estudiante con ID {id}.";

        if (_db.Inscripciones.Exists(i => i.EstudianteId == id))
            return "Error: No se puede eliminar el estudiante porque tiene inscripciones registradas.";

        _db.Estudiantes.Delete(id);

        return $"Éxito: Estudiante con ID {id} eliminado correctamente.";
    }
}
