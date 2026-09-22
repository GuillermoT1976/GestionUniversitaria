using GestionUniversitaria.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionUniversitaria.Controllers;

public class EstudiantesController : Controller
{
    private readonly EstudianteService _estudianteService;

    public EstudiantesController(EstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    public IActionResult Index()
    {
        var estudiantes = _estudianteService.ObtenerTodos();

        return View(estudiantes);
    }
}