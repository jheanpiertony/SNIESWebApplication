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
    public class CicloVitalServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CicloVitalServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.CicloVitalServicioExtension.ToListAsync());
        }

        // GET: CicloVitalServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalServicioExtension cicloVitalServicioExtension = await db.CicloVitalServicioExtension.FindAsync(id);
            if (cicloVitalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalServicioExtension);
        }

        // GET: CicloVitalServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CicloVitalServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,CICLO_VITAL,FECHA_PERIODO")] CicloVitalServicioExtension cicloVitalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.CicloVitalServicioExtension.Add(cicloVitalServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cicloVitalServicioExtension);
        }

        // GET: CicloVitalServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalServicioExtension cicloVitalServicioExtension = await db.CicloVitalServicioExtension.FindAsync(id);
            if (cicloVitalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalServicioExtension);
        }

        // POST: CicloVitalServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,CICLO_VITAL,FECHA_PERIODO")] CicloVitalServicioExtension cicloVitalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cicloVitalServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cicloVitalServicioExtension);
        }

        // GET: CicloVitalServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalServicioExtension cicloVitalServicioExtension = await db.CicloVitalServicioExtension.FindAsync(id);
            if (cicloVitalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalServicioExtension);
        }

        // POST: CicloVitalServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CicloVitalServicioExtension cicloVitalServicioExtension = await db.CicloVitalServicioExtension.FindAsync(id);
            db.CicloVitalServicioExtension.Remove(cicloVitalServicioExtension);
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
