using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppContactosG60.Models;
using AppContactosG60.Data;
using Microsoft.EntityFrameworkCore;


namespace AppContactosG60.Controllers;

public class HomeController : Controller
{
    private readonly AppDBContext _contexto; 

    public HomeController(AppDBContext contexto) 
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> Index() // 6 Instruccion
    {
        return View(await _contexto.Contacto.ToListAsync());
    }
    
    [HttpGet]
    public IActionResult Crear() 
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Contacto contacto) // 7 Instruccion
    {
        if (ModelState.IsValid)
        {
            _contexto.Contacto.Add(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
