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
    public class ConvenioInternacionalInstitucionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConvenioInternacionalInstitucion
        public async Task<ActionResult> Index()
        {
            return View(await db.ConvenioInternacionalInstitucion.ToListAsync());
        }

        // GET: ConvenioInternacionalInstitucion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacionalInstitucion convenioInternacionalInstitucion = await db.ConvenioInternacionalInstitucion.FindAsync(id);
            if (convenioInternacionalInstitucion == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacionalInstitucion);
        }

        // GET: ConvenioInternacionalInstitucion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConvenioInternacionalInstitucion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_CONVENIO,INSTITUCION_ASOCIADA,INSTITUCION_MULTILATERAL,ID_PAIS_INSTITUCION_ASOCIADA,FECHA_PERIODO")] ConvenioInternacionalInstitucion convenioInternacionalInstitucion)
        {
            if (ModelState.IsValid)
            {
                db.ConvenioInternacionalInstitucion.Add(convenioInternacionalInstitucion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(convenioInternacionalInstitucion);
        }

        // GET: ConvenioInternacionalInstitucion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacionalInstitucion convenioInternacionalInstitucion = await db.ConvenioInternacionalInstitucion.FindAsync(id);
            if (convenioInternacionalInstitucion == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacionalInstitucion);
        }

        // POST: ConvenioInternacionalInstitucion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_CONVENIO,INSTITUCION_ASOCIADA,INSTITUCION_MULTILATERAL,ID_PAIS_INSTITUCION_ASOCIADA,FECHA_PERIODO")] ConvenioInternacionalInstitucion convenioInternacionalInstitucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convenioInternacionalInstitucion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(convenioInternacionalInstitucion);
        }

        // GET: ConvenioInternacionalInstitucion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacionalInstitucion convenioInternacionalInstitucion = await db.ConvenioInternacionalInstitucion.FindAsync(id);
            if (convenioInternacionalInstitucion == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacionalInstitucion);
        }

        // POST: ConvenioInternacionalInstitucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConvenioInternacionalInstitucion convenioInternacionalInstitucion = await db.ConvenioInternacionalInstitucion.FindAsync(id);
            db.ConvenioInternacionalInstitucion.Remove(convenioInternacionalInstitucion);
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
