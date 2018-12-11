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
    public class FteNacionEventoCulturalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FteNacionEventoCultural
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "FteNacionEventoCultural";
            var PeriodoIdActual = db.FteNacionEventoCultural.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.FteNacionEventoCultural.OrderBy(x => new { x.FECHA_PERIODO, x.CODIGO_EVENTO }).ToListAsync());
        }

        // GET: FteNacionEventoCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteNacionEventoCultural fteNacionEventoCultural = await db.FteNacionEventoCultural.FindAsync(id);
            if (fteNacionEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteNacionEventoCultural);
        }

        // GET: FteNacionEventoCultural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FteNacionEventoCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,ID_FUENTE_NACIONAL,FUENTE_NACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FteNacionEventoCultural fteNacionEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.FteNacionEventoCultural.Add(fteNacionEventoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fteNacionEventoCultural);
        }

        // GET: FteNacionEventoCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteNacionEventoCultural fteNacionEventoCultural = await db.FteNacionEventoCultural.FindAsync(id);
            if (fteNacionEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteNacionEventoCultural);
        }

        // POST: FteNacionEventoCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,ID_FUENTE_NACIONAL,FUENTE_NACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FteNacionEventoCultural fteNacionEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fteNacionEventoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fteNacionEventoCultural);
        }

        // GET: FteNacionEventoCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FteNacionEventoCultural fteNacionEventoCultural = await db.FteNacionEventoCultural.FindAsync(id);
            if (fteNacionEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(fteNacionEventoCultural);
        }

        // POST: FteNacionEventoCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FteNacionEventoCultural fteNacionEventoCultural = await db.FteNacionEventoCultural.FindAsync(id);
            db.FteNacionEventoCultural.Remove(fteNacionEventoCultural);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();
            var lista = db.FteNacionEventoCultural.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
