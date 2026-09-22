using LiteDB;
using GestionUniversitaria.Models;

namespace GestionUniversitaria.Data;

public class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _db;

    public LiteDbContext(string path = "universidad.db")
    {
        _db = new LiteDatabase($"Filename={path};Connection=shared");
        EnsureIndexes();
    }

    public ILiteCollection<Carrera> Carreras => _db.GetCollection<Carrera>("carreras");
    public ILiteCollection<Estudiante> Estudiantes => _db.GetCollection<Estudiante>("estudiantes");
    public ILiteCollection<Materia> Materias => _db.GetCollection<Materia>("materias");
    public ILiteCollection<Profesor> Profesores => _db.GetCollection<Profesor>("profesores");
    public ILiteCollection<Inscripcion> Inscripciones => _db.GetCollection<Inscripcion>("inscripciones");

    public void EnsureIndexes()
    {
        Estudiantes.EnsureIndex(e => e.Matricula, true);
        Estudiantes.EnsureIndex(e => e.Email, true);
        Profesores.EnsureIndex(p => p.Email, true);
        Materias.EnsureIndex(m => m.Codigo, true);
    }

    public void Seed()
    {
        if (Carreras.Count() > 0)
            return;

        List<Carrera> carreras = new()
        {
            new Carrera { Id = 0, Nombre = "Ingeniería en Sistemas", Facultad = "Facultad de Ingeniería", DuracionAnios = 5 },
            new Carrera { Id = 0, Nombre = "Medicina", Facultad = "Facultad de Ciencias Médicas", DuracionAnios = 6 },
            new Carrera { Id = 0, Nombre = "Abogacía", Facultad = "Facultad de Derecho", DuracionAnios = 5 },
            new Carrera { Id = 0, Nombre = "Licenciatura en Administración", Facultad = "Facultad de Ciencias Económicas", DuracionAnios = 4 }
        };
        Carreras.InsertBulk(carreras);

        List<Profesor> profesores = new()
        {
            new Profesor { Id = 0, Nombre = "María", Apellido = "García", Email = "mgarcia@uni.edu", Especialidad = "Matemáticas" },
            new Profesor { Id = 0, Nombre = "Carlos", Apellido = "Rodríguez", Email = "crodriguez@uni.edu", Especialidad = "Programación" },
            new Profesor { Id = 0, Nombre = "Laura", Apellido = "Martínez", Email = "lmartinez@uni.edu", Especialidad = "Bases de Datos" },
            new Profesor { Id = 0, Nombre = "Pedro", Apellido = "López", Email = "plopez@uni.edu", Especialidad = "Física" }
        };
        Profesores.InsertBulk(profesores);

        List<Materia> materias = new()
        {
            new Materia { Id = 0, Codigo = "MAT101", Nombre = "Análisis Matemático I", Creditos = 8, CarreraId = 1 },
            new Materia { Id = 0, Codigo = "PRG101", Nombre = "Programación I", Creditos = 6, CarreraId = 1 },
            new Materia { Id = 0, Codigo = "BDD201", Nombre = "Bases de Datos", Creditos = 7, CarreraId = 1 },
            new Materia { Id = 0, Codigo = "FIS101", Nombre = "Física General", Creditos = 8, CarreraId = 1 },
            new Materia { Id = 0, Codigo = "ANA101", Nombre = "Anatomía Humana", Creditos = 10, CarreraId = 2 },
            new Materia { Id = 0, Codigo = "DER101", Nombre = "Derecho Civil", Creditos = 6, CarreraId = 3 }
        };
        Materias.InsertBulk(materias);

        List<Estudiante> estudiantes = new()
        {
            new Estudiante { Id = 0, Matricula = "2024-001", Nombre = "Juan", Apellido = "García", Email = "juan.garcia@alumno.edu", FechaNacimiento = new DateTime(2000, 5, 12), CarreraId = 1, Activo = true },
            new Estudiante { Id = 0, Matricula = "2024-002", Nombre = "Ana", Apellido = "Fernández", Email = "ana.fernandez@alumno.edu", FechaNacimiento = new DateTime(2001, 8, 23), CarreraId = 1, Activo = true },
            new Estudiante { Id = 0, Matricula = "2024-003", Nombre = "Luis", Apellido = "Pérez", Email = "luis.perez@alumno.edu", FechaNacimiento = new DateTime(1999, 3, 7), CarreraId = 2, Activo = true },
            new Estudiante { Id = 0, Matricula = "2024-004", Nombre = "Sofía", Apellido = "Ramírez", Email = "sofia.ramirez@alumno.edu", FechaNacimiento = new DateTime(2002, 11, 30), CarreraId = 3, Activo = true },
            new Estudiante { Id = 0, Matricula = "2024-005", Nombre = "Diego", Apellido = "Gómez", Email = "diego.gomez@alumno.edu", FechaNacimiento = new DateTime(2000, 1, 18), CarreraId = 4, Activo = true }
        };
        Estudiantes.InsertBulk(estudiantes);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
