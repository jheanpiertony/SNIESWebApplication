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
    public class FteInternEventoCulturalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FteInternEventoCultural
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "FteInternEventoCultural";
            var PeriodoIdActual = db.FteInternEventoCultural.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.FteInternEventoCultural.OrderBy(x => new { x.FECHA_PERIODO, x.CODIGO_EVENTO }).ToListAsync());
        }

        // GET: FteInternEventoCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteInternEventoCultural fteInternEventoCultural = await db.FteInternEventoCultural.FindAsync(id);
            if (fteInternEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteInternEventoCultural);
        }

        // GET: FteInternEventoCultural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FteInternEventoCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,NOMBRE_INSTITUCION,ID_PAIS,PAIS,ID_FUENTE_INTERNACIONAL,FUENTE_INTERNACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FteInternEventoCultural fteInternEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.FteInternEventoCultural.Add(fteInternEventoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fteInternEventoCultural);
        }

        // GET: FteInternEventoCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteInternEventoCultural fteInternEventoCultural = await db.FteInternEventoCultural.FindAsync(id);
            if (fteInternEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteInternEventoCultural);
        }

        // POST: FteInternEventoCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,NOMBRE_INSTITUCION,ID_PAIS,PAIS,ID_FUENTE_INTERNACIONAL,FUENTE_INTERNACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FteInternEventoCultural fteInternEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fteInternEventoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fteInternEventoCultural);
        }

        // GET: FteInternEventoCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteInternEventoCultural fteInternEventoCultural = await db.FteInternEventoCultural.FindAsync(id);
            if (fteInternEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteInternEventoCultural);
        }

        // POST: FteInternEventoCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FteInternEventoCultural fteInternEventoCultural = await db.FteInternEventoCultural.FindAsync(id);
            db.FteInternEventoCultural.Remove(fteInternEventoCultural);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.FteInternEventoCultural.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
