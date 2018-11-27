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
    public class OtraEntidadProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OtraEntidadProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.OtraEntidadProyectoExtencion.ToListAsync());
        }

        // GET: OtraEntidadProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadProyectoExtencion otraEntidadProyectoExtencion = await db.OtraEntidadProyectoExtencion.FindAsync(id);
            if (otraEntidadProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadProyectoExtencion);
        }

        // GET: OtraEntidadProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtraEntidadProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,NOMBRE_ENTIDAD,PAIS,SECTOR_ENTIDAD,FECHA_PERIODO")] OtraEntidadProyectoExtencion otraEntidadProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.OtraEntidadProyectoExtencion.Add(otraEntidadProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(otraEntidadProyectoExtencion);
        }

        // GET: OtraEntidadProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadProyectoExtencion otraEntidadProyectoExtencion = await db.OtraEntidadProyectoExtencion.FindAsync(id);
            if (otraEntidadProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadProyectoExtencion);
        }

        // POST: OtraEntidadProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,NOMBRE_ENTIDAD,PAIS,SECTOR_ENTIDAD,FECHA_PERIODO")] OtraEntidadProyectoExtencion otraEntidadProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otraEntidadProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(otraEntidadProyectoExtencion);
        }

        // GET: OtraEntidadProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadProyectoExtencion otraEntidadProyectoExtencion = await db.OtraEntidadProyectoExtencion.FindAsync(id);
            if (otraEntidadProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadProyectoExtencion);
        }

        // POST: OtraEntidadProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtraEntidadProyectoExtencion otraEntidadProyectoExtencion = await db.OtraEntidadProyectoExtencion.FindAsync(id);
            db.OtraEntidadProyectoExtencion.Remove(otraEntidadProyectoExtencion);
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
