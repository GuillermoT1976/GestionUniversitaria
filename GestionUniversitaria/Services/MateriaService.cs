using GestionUniversitaria.Data;
using GestionUniversitaria.Models;

namespace GestionUniversitaria.Services;

public class MateriaService
{
    private readonly LiteDbContext _db;

    public MateriaService(LiteDbContext db)
    {
        _db = db;
    }

    public string AgregarMateria(Materia nuevaMateria)
    {
        if (string.IsNullOrWhiteSpace(nuevaMateria.Codigo) ||
            string.IsNullOrWhiteSpace(nuevaMateria.Nombre) ||
            nuevaMateria.Creditos <= 0)
        {
            return "Error: Código, Nombre y Créditos son obligatorios. Los créditos deben ser mayores a 0.";
        }

        if (_db.Materias.Exists(m => m.Codigo == nuevaMateria.Codigo))
        {
            return $"Error: Ya existe una materia con el código {nuevaMateria.Codigo}.";
        }

        if (_db.Carreras.FindById(nuevaMateria.CarreraId) is null)
        {
            return $"Error: La carrera con ID {nuevaMateria.CarreraId} no existe.";
        }

        _db.Materias.Insert(nuevaMateria);

        return $"Éxito: Materia {nuevaMateria.Nombre} agregada correctamente con ID {nuevaMateria.Id}.";
    }

    public List<Materia> ObtenerTodas()
    {
        return _db.Materias.FindAll().ToList();
    }

    public Materia? ObtenerPorId(int id)
    {
        return _db.Materias.FindById(id);
    }

    public string ActualizarMateria(Materia entidadActualizada)
    {
        Materia? existente = _db.Materias.FindById(entidadActualizada.Id);
        if (existente is null)
            return $"Error: No existe Materia con ID {entidadActualizada.Id}.";

        if (string.IsNullOrWhiteSpace(entidadActualizada.Codigo) ||
            string.IsNullOrWhiteSpace(entidadActualizada.Nombre) ||
            entidadActualizada.Creditos <= 0)
        {
            return "Error: Código, Nombre y Créditos son obligatorios. Los créditos deben ser mayores a 0.";
        }

        if (_db.Materias.Exists(m => m.Id != entidadActualizada.Id && m.Codigo == entidadActualizada.Codigo))
            return $"Error: Ya existe una materia con el código {entidadActualizada.Codigo}.";

        if (_db.Carreras.FindById(entidadActualizada.CarreraId) is null)
            return $"Error: La carrera con ID {entidadActualizada.CarreraId} no existe.";

        _db.Materias.Update(entidadActualizada);

        return $"Éxito: Materia con ID {entidadActualizada.Id} actualizada correctamente.";
    }

    public string EliminarMateria(int id)
    {
        Materia? existente = _db.Materias.FindById(id);
        if (existente is null)
            return $"Error: No existe Materia con ID {id}.";

        if (_db.Inscripciones.Exists(i => i.MateriaId == id))
            return "Error: No se puede eliminar la materia porque tiene inscripciones registradas.";

        _db.Materias.Delete(id);

        return $"Éxito: Materia con ID {id} eliminada correctamente.";
    }
}
