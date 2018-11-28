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
    public class EntidadNacionalServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntidadNacionalServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.EntidadNacionalServicioExtension.ToListAsync());
        }

        // GET: EntidadNacionalServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalServicioExtension entidadNacionalServicioExtension = await db.EntidadNacionalServicioExtension.FindAsync(id);
            if (entidadNacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalServicioExtension);
        }

        // GET: EntidadNacionalServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadNacionalServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ENTIDAD_NACIONAL,FECHA_PERIODO")] EntidadNacionalServicioExtension entidadNacionalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.EntidadNacionalServicioExtension.Add(entidadNacionalServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entidadNacionalServicioExtension);
        }

        // GET: EntidadNacionalServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalServicioExtension entidadNacionalServicioExtension = await db.EntidadNacionalServicioExtension.FindAsync(id);
            if (entidadNacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalServicioExtension);
        }

        // POST: EntidadNacionalServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ENTIDAD_NACIONAL,FECHA_PERIODO")] EntidadNacionalServicioExtension entidadNacionalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidadNacionalServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(entidadNacionalServicioExtension);
        }

        // GET: EntidadNacionalServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalServicioExtension entidadNacionalServicioExtension = await db.EntidadNacionalServicioExtension.FindAsync(id);
            if (entidadNacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalServicioExtension);
        }

        // POST: EntidadNacionalServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EntidadNacionalServicioExtension entidadNacionalServicioExtension = await db.EntidadNacionalServicioExtension.FindAsync(id);
            db.EntidadNacionalServicioExtension.Remove(entidadNacionalServicioExtension);
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
