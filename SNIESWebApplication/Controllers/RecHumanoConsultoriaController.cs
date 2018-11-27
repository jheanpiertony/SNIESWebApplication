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
    public class RecHumanoConsultoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecHumanoConsultoria
        public async Task<ActionResult> Index()
        {
            return View(await db.RecHumanoConsultoria.ToListAsync());
        }

        // GET: RecHumanoConsultoria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoConsultoria recHumanoConsultoria = await db.RecHumanoConsultoria.FindAsync(id);
            if (recHumanoConsultoria == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoConsultoria);
        }

        // GET: RecHumanoConsultoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecHumanoConsultoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_CONSULTORIA,DESCRIPCION_CONSULTORIA,ID_TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,NIVEL_ESTUDIO,FECHA_PERIODO")] RecHumanoConsultoria recHumanoConsultoria)
        {
            if (ModelState.IsValid)
            {
                db.RecHumanoConsultoria.Add(recHumanoConsultoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recHumanoConsultoria);
        }

        // GET: RecHumanoConsultoria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoConsultoria recHumanoConsultoria = await db.RecHumanoConsultoria.FindAsync(id);
            if (recHumanoConsultoria == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoConsultoria);
        }

        // POST: RecHumanoConsultoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_CONSULTORIA,DESCRIPCION_CONSULTORIA,ID_TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,NIVEL_ESTUDIO,FECHA_PERIODO")] RecHumanoConsultoria recHumanoConsultoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recHumanoConsultoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recHumanoConsultoria);
        }

        // GET: RecHumanoConsultoria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoConsultoria recHumanoConsultoria = await db.RecHumanoConsultoria.FindAsync(id);
            if (recHumanoConsultoria == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoConsultoria);
        }

        // POST: RecHumanoConsultoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecHumanoConsultoria recHumanoConsultoria = await db.RecHumanoConsultoria.FindAsync(id);
            db.RecHumanoConsultoria.Remove(recHumanoConsultoria);
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
