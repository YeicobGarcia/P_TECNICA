using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using P_TECNICA.Models;

namespace P_TECNICA.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosModel _context;

        readonly pruebaContext user_context;

        public EmpleadosController(EmpleadosModel context, pruebaContext contexto)
        {
            _context = context;
            user_context = contexto;
        }


        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
            //
        }

        // GET: Empleados/Details/
        public async Task<IActionResult> Details_e(uint? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FirstOrDefaultAsync(m => m.idEmpleados == id);
            var userC = Convert.ToInt32(empleado.userUdpdate);
            var usuario = await user_context.Usuarios.FirstOrDefaultAsync(m => m.IdUsuarios == userC);

            var userCreate = Convert.ToString(usuario.Username);
            var SBase = Convert.ToInt32(empleado.SalarioB);
            var Hijos = Convert.ToInt32(empleado.Hijos);

            var BonoD = 250;

            var IGGS = SBase * 4.83 / 100;
            var IRTRA = SBase * 1 / 100;
            var BonoP = Hijos * 133;
            var SalarioT = BonoD + BonoP + SBase;
            var SalarioL = SalarioT - IGGS - IRTRA;

            ViewData["IGGS"] = IGGS;
            ViewData["IRTRA"] = IRTRA;
            ViewData["BonoP"] = BonoP;
            ViewData["SalarioT"] = SalarioT;
            ViewData["SalarioL"] = SalarioL;
            ViewData["BonoD"] = BonoD;
            ViewData["userCreate"] = userCreate;

            if (empleado == null)
            {
                return NotFound();
            }

           return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create_e()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_e([Bind("idEmpleados,NombreE,DPI,Hijos,SalarioB,userUdpdate,fechaUpdate")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/
        public async Task<IActionResult> Edit_e(uint? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_e(uint id, [Bind("idEmpleados,NombreE,DPI,Hijos,SalarioB,userUdpdate,fechaUpdate")] Empleado empleado)
        {
            if (id != empleado.idEmpleados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.idEmpleados))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/
        public async Task<IActionResult> Delete_e(uint? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.idEmpleados == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // POST: Empleados/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'pruebaContext.Usuarios'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(uint id)
        {
            return _context.Empleados.Any(e => e.idEmpleados == id);
        }
    }
}
