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
    public class PoblacionCondiProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoblacionCondiProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.PoblacionCondiProyectoExtencion.ToListAsync());
        }

        // GET: PoblacionCondiProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion = await db.PoblacionCondiProyectoExtencion.FindAsync(id);
            if (poblacionCondiProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionCondiProyectoExtencion);
        }

        // GET: PoblacionCondiProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoblacionCondiProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,POBLACION,CANTIDAD,FECHA_PERIODO")] PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.PoblacionCondiProyectoExtencion.Add(poblacionCondiProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(poblacionCondiProyectoExtencion);
        }

        // GET: PoblacionCondiProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion = await db.PoblacionCondiProyectoExtencion.FindAsync(id);
            if (poblacionCondiProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionCondiProyectoExtencion);
        }

        // POST: PoblacionCondiProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,POBLACION,CANTIDAD,FECHA_PERIODO")] PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poblacionCondiProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(poblacionCondiProyectoExtencion);
        }

        // GET: PoblacionCondiProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion = await db.PoblacionCondiProyectoExtencion.FindAsync(id);
            if (poblacionCondiProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionCondiProyectoExtencion);
        }

        // POST: PoblacionCondiProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PoblacionCondiProyectoExtencion poblacionCondiProyectoExtencion = await db.PoblacionCondiProyectoExtencion.FindAsync(id);
            db.PoblacionCondiProyectoExtencion.Remove(poblacionCondiProyectoExtencion);
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
