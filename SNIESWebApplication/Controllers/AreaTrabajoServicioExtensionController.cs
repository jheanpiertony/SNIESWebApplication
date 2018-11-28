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
    public class AreaTrabajoServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AreaTrabajoServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.AreaTrabajoServicioExtension.ToListAsync());
        }

        // GET: AreaTrabajoServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaTrabajoServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoServicioExtension areaTrabajoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.AreaTrabajoServicioExtension.Add(areaTrabajoServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // POST: AreaTrabajoServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoServicioExtension areaTrabajoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaTrabajoServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // POST: AreaTrabajoServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            db.AreaTrabajoServicioExtension.Remove(areaTrabajoServicioExtension);
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
