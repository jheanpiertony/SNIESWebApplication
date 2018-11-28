namespace SNIESWebApplication.Controllers
{
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

    public class ContratoDocenteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContratoDocente
        public async Task<ActionResult> Index()
        {
            return View(await db.ContratoDocente.ToListAsync());
        }

        // GET: ContratoDocente/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDocente contratoDocente = await db.ContratoDocente.FindAsync(id);
            if (contratoDocente == null)
            {
                return HttpNotFound();
            }
            return View(contratoDocente);
        }

        // GET: ContratoDocente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContratoDocente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PORCENTAJE_DOCENCIA,PORCENTAJE_INVESTIGACION,PORCENTAJE_ADMINISTRATIVA,PORCENTAJE_EXTENSION,PORCENTAJE_OTRAS_ACTIVIDADES,HORAS_DEDICACION_SEMESTRE,DEDICACION,TIPO_CONTRATO,ASIGNACION_BASICA_MENSUAL,FECHA_PERIODO")] ContratoDocente contratoDocente)
        {
            if (ModelState.IsValid)
            {
                db.ContratoDocente.Add(contratoDocente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contratoDocente);
        }

        // GET: ContratoDocente/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDocente contratoDocente = await db.ContratoDocente.FindAsync(id);
            if (contratoDocente == null)
            {
                return HttpNotFound();
            }
            return View(contratoDocente);
        }

        // POST: ContratoDocente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PORCENTAJE_DOCENCIA,PORCENTAJE_INVESTIGACION,PORCENTAJE_ADMINISTRATIVA,PORCENTAJE_EXTENSION,PORCENTAJE_OTRAS_ACTIVIDADES,HORAS_DEDICACION_SEMESTRE,DEDICACION,TIPO_CONTRATO,ASIGNACION_BASICA_MENSUAL,FECHA_PERIODO")] ContratoDocente contratoDocente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratoDocente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contratoDocente);
        }

        // GET: ContratoDocente/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDocente contratoDocente = await db.ContratoDocente.FindAsync(id);
            if (contratoDocente == null)
            {
                return HttpNotFound();
            }
            return View(contratoDocente);
        }

        // POST: ContratoDocente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContratoDocente contratoDocente = await db.ContratoDocente.FindAsync(id);
            db.ContratoDocente.Remove(contratoDocente);
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
