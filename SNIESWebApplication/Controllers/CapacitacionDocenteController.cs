

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
    using SNIESWebApplication.Helpers;
    using ClosedXML.Excel;
    using System.IO;

    [Authorize(Roles = "Administrador, Desarrollador, Calidad")]
    public class CapacitacionDocenteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CapacitacionDocente
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "CapacitacionDocente";
            var PeriodoIdActual = db.CapacitacionDocente.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
            int i = 0;
            var listaPeriodo = new List<Periodo>();
            foreach (var item in PeriodoIdActual)
            {
                if (item.Key != null)
                {
                    listaPeriodo.Add(new Periodo() { Id = i++, FechaPeriodo = item.Key.ToString() });
                }
            }
            ViewBag.PeriodoIdActual = new SelectList(listaPeriodo, "Id", "FechaPeriodo");
            return View(await db.CapacitacionDocente.OrderBy(x => new { x.FECHA_PERIODO, x.NUMERO_DOCUMENTO }).ToListAsync());
        }

        // GET: CapacitacionDocente/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapacitacionDocente capacitacionDocente = await db.CapacitacionDocente.FindAsync(id);
            if (capacitacionDocente == null)
            {
                return HttpNotFound();
            }
            return View(capacitacionDocente);
        }

        // GET: CapacitacionDocente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CapacitacionDocente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,ID_TIPO_CAPACITACION,TIPO_CAPACITACION,NO_HORAS_CURSADAS,ID_TIPO_CURSO,TIPO_CURSO,ID_TEMA_CURSO,TEMA_CURSO,ID_PAIS,PAIS,NOMBRE_CURSO,FECHA_PERIODO")] CapacitacionDocente capacitacionDocente)
        {
            if (ModelState.IsValid)
            {
                db.CapacitacionDocente.Add(capacitacionDocente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(capacitacionDocente);
        }

        // GET: CapacitacionDocente/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapacitacionDocente capacitacionDocente = await db.CapacitacionDocente.FindAsync(id);
            if (capacitacionDocente == null)
            {
                return HttpNotFound();
            }
            return View(capacitacionDocente);
        }

        // POST: CapacitacionDocente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,ID_TIPO_CAPACITACION,TIPO_CAPACITACION,NO_HORAS_CURSADAS,ID_TIPO_CURSO,TIPO_CURSO,ID_TEMA_CURSO,TEMA_CURSO,ID_PAIS,PAIS,NOMBRE_CURSO,FECHA_PERIODO")] CapacitacionDocente capacitacionDocente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capacitacionDocente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(capacitacionDocente);
        }

        // GET: CapacitacionDocente/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapacitacionDocente capacitacionDocente = await db.CapacitacionDocente.FindAsync(id);
            if (capacitacionDocente == null)
            {
                return HttpNotFound();
            }
            return View(capacitacionDocente);
        }

        // POST: CapacitacionDocente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CapacitacionDocente capacitacionDocente = await db.CapacitacionDocente.FindAsync(id);
            db.CapacitacionDocente.Remove(capacitacionDocente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.CapacitacionDocente.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = dt.TableName.ToString();

            using (XLWorkbook wb = new XLWorkbook())//https://github.com/ClosedXML/ClosedXML <----- la libreria
            {
                wb.Worksheets.Add(dt, nombre);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + nombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
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
