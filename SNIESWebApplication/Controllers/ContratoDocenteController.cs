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

    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co")]
    public class ContratoDocenteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContratoDocente
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "ContratoDocente";
            var PeriodoIdActual = db.ContratoDocente.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.ContratoDocente.OrderBy(x => new { x.FECHA_PERIODO, x.NUMERO_DOCUMENTO }).ToListAsync());
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

        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();
            var lista = db.ContratoDocente.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
