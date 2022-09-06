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
    
    // Crear 

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
            //  Metodo Add lo utilizamo para crear registros en la base de datos (INSERT INTO contacto VALUES ()
            _contexto.Contacto.Add(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    // Editar

    [HttpGet]
    public IActionResult Editar(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }

        var contacto = _contexto.Contacto.Find(id); // SQL (SELECT * FROM Contacto WHERE id = 1)

        if(contacto == null)
        {
            return NotFound();
        }

        return View(contacto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Contacto contacto) //
    {
        if (ModelState.IsValid)
        {
            _contexto.Contacto.Update(contacto); 
        
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    // Detalle
    [HttpGet]
    public IActionResult Detalle(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }

        var contacto = _contexto.Contacto.Find(id); // SQL (SELECT * FROM Contacto WHERE id = 1)

        if(contacto == null)
        {
            return NotFound();
        }

        return View(contacto);
    }

    // Borrar
    [HttpGet]
    public IActionResult Borrar(int? id) 
    {
        if(id == null)
        {
            return NotFound();
        }
        var contacto = _contexto.Contacto.Find(id);

        if(contacto == null)
        {
            return NotFound();
        }

        return View(contacto);

    }


    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarContacto(int? id)
    {
        var contacto = await _contexto.Contacto.FindAsync(id);

        if (contacto == null)
        {
            return View();
        }

        _contexto.Contacto.Remove(contacto); 
        await _contexto.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));

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
