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

namespace SNIESWebApplication.Controllers
{
    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co,jgomezm@unicoc.edu.co")]
    public class RecHumanoEventoCulturalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecHumanoEventoCultural
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "RecHumanoEventoCultural";
            var PeriodoIdActual = db.RecHumanoEventoCultural.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.RecHumanoEventoCultural.OrderBy(x => new { x.FECHA_PERIODO, x.NUMERO_DOCUMENTO }).ToListAsync());
        }

        // GET: RecHumanoEventoCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoEventoCultural recHumanoEventoCultural = await db.RecHumanoEventoCultural.FindAsync(id);
            if (recHumanoEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoEventoCultural);
        }

        // GET: RecHumanoEventoCultural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecHumanoEventoCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,DEDICACION,FECHA_PERIODO")] RecHumanoEventoCultural recHumanoEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.RecHumanoEventoCultural.Add(recHumanoEventoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recHumanoEventoCultural);
        }

        // GET: RecHumanoEventoCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoEventoCultural recHumanoEventoCultural = await db.RecHumanoEventoCultural.FindAsync(id);
            if (recHumanoEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoEventoCultural);
        }

        // POST: RecHumanoEventoCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,DEDICACION,FECHA_PERIODO")] RecHumanoEventoCultural recHumanoEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recHumanoEventoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recHumanoEventoCultural);
        }

        // GET: RecHumanoEventoCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoEventoCultural recHumanoEventoCultural = await db.RecHumanoEventoCultural.FindAsync(id);
            if (recHumanoEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoEventoCultural);
        }

        // POST: RecHumanoEventoCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecHumanoEventoCultural recHumanoEventoCultural = await db.RecHumanoEventoCultural.FindAsync(id);
            db.RecHumanoEventoCultural.Remove(recHumanoEventoCultural);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.RecHumanoEventoCultural.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
