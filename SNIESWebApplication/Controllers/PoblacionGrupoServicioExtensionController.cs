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
    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co,jgomezm@unicoc.edu.co")]
    public class PoblacionGrupoServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoblacionGrupoServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.PoblacionGrupoServicioExtension.ToListAsync());
        }

        // GET: PoblacionGrupoServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoServicioExtension poblacionGrupoServicioExtension = await db.PoblacionGrupoServicioExtension.FindAsync(id);
            if (poblacionGrupoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoServicioExtension);
        }

        // GET: PoblacionGrupoServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoblacionGrupoServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,POBLACION_GRUPO,CANTIDAD,FECHA_PERIODO")] PoblacionGrupoServicioExtension poblacionGrupoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.PoblacionGrupoServicioExtension.Add(poblacionGrupoServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(poblacionGrupoServicioExtension);
        }

        // GET: PoblacionGrupoServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoServicioExtension poblacionGrupoServicioExtension = await db.PoblacionGrupoServicioExtension.FindAsync(id);
            if (poblacionGrupoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoServicioExtension);
        }

        // POST: PoblacionGrupoServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,POBLACION_GRUPO,CANTIDAD,FECHA_PERIODO")] PoblacionGrupoServicioExtension poblacionGrupoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poblacionGrupoServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(poblacionGrupoServicioExtension);
        }

        // GET: PoblacionGrupoServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoServicioExtension poblacionGrupoServicioExtension = await db.PoblacionGrupoServicioExtension.FindAsync(id);
            if (poblacionGrupoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoServicioExtension);
        }

        // POST: PoblacionGrupoServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PoblacionGrupoServicioExtension poblacionGrupoServicioExtension = await db.PoblacionGrupoServicioExtension.FindAsync(id);
            db.PoblacionGrupoServicioExtension.Remove(poblacionGrupoServicioExtension);
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
