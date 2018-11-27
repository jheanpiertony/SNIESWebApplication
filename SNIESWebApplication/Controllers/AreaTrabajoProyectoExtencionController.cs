using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SNIESWebApplication.Models;

namespace SNIESWebApplication.Controllers
{
    public class AreaTrabajoProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AreaTrabajoProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.AreaTrabajoProyectoExtencion.ToListAsync());
        }

        // GET: AreaTrabajoProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion = await db.AreaTrabajoProyectoExtencion.FindAsync(id);
            if (areaTrabajoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoProyectoExtencion);
        }

        // GET: AreaTrabajoProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaTrabajoProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.AreaTrabajoProyectoExtencion.Add(areaTrabajoProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(areaTrabajoProyectoExtencion);
        }

        // GET: AreaTrabajoProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion = await db.AreaTrabajoProyectoExtencion.FindAsync(id);
            if (areaTrabajoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoProyectoExtencion);
        }

        // POST: AreaTrabajoProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaTrabajoProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(areaTrabajoProyectoExtencion);
        }

        // GET: AreaTrabajoProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion = await db.AreaTrabajoProyectoExtencion.FindAsync(id);
            if (areaTrabajoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoProyectoExtencion);
        }

        // POST: AreaTrabajoProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AreaTrabajoProyectoExtencion areaTrabajoProyectoExtencion = await db.AreaTrabajoProyectoExtencion.FindAsync(id);
            db.AreaTrabajoProyectoExtencion.Remove(areaTrabajoProyectoExtencion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
