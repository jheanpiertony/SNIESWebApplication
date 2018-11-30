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

    public class InscritoProgramaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InscritoPrograma
        public async Task<ActionResult> Index()
        {
            return View(await db.InscritoPrograma.ToListAsync());
        }

        // GET: InscritoPrograma/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscritoPrograma inscritoPrograma = await db.InscritoPrograma.FindAsync(id);
            if (inscritoPrograma == null)
            {
                return HttpNotFound();
            }
            return View(inscritoPrograma);
        }

        // GET: InscritoPrograma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InscritoPrograma/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA,COD_DANE,DEPARTAMENTO,MUNICIPIO,FECHA_PERIODO")] InscritoPrograma inscritoPrograma)
        {
            if (ModelState.IsValid)
            {
                db.InscritoPrograma.Add(inscritoPrograma);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inscritoPrograma);
        }

        // GET: InscritoPrograma/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscritoPrograma inscritoPrograma = await db.InscritoPrograma.FindAsync(id);
            if (inscritoPrograma == null)
            {
                return HttpNotFound();
            }
            return View(inscritoPrograma);
        }

        // POST: InscritoPrograma/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA,COD_DANE,DEPARTAMENTO,MUNICIPIO,FECHA_PERIODO")] InscritoPrograma inscritoPrograma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscritoPrograma).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inscritoPrograma);
        }

        // GET: InscritoPrograma/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscritoPrograma inscritoPrograma = await db.InscritoPrograma.FindAsync(id);
            if (inscritoPrograma == null)
            {
                return HttpNotFound();
            }
            return View(inscritoPrograma);
        }

        // POST: InscritoPrograma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InscritoPrograma inscritoPrograma = await db.InscritoPrograma.FindAsync(id);
            db.InscritoPrograma.Remove(inscritoPrograma);
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
