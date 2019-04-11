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
    [Authorize(Roles = "Administrador, Desarrollador, Calidad")]
    public class DocenteEducacionContinuaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DocenteEducacionContinua
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "DocenteEducacionContinua";
            var PeriodoIdActual = db.DocenteEducacionContinua.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
            int i = 0;
            var listaPeriodo = new List<Periodo>();
            foreach (var item in PeriodoIdActual)
            {

                listaPeriodo.Add(new Periodo() { Id = i++, FechaPeriodo = item.Key.ToString() });
            }
            ViewBag.PeriodoIdActual = new SelectList(listaPeriodo, "Id", "FechaPeriodo");
            return View(await db.DocenteEducacionContinua.ToListAsync());
        }

        // GET: DocenteEducacionContinua/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteEducacionContinua docenteEducacionContinua = await db.DocenteEducacionContinua.FindAsync(id);
            if (docenteEducacionContinua == null)
            {
                return HttpNotFound();
            }
            return View(docenteEducacionContinua);
        }

        // GET: DocenteEducacionContinua/Create
        public ActionResult Create()
        {
            //ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: DocenteEducacionContinua/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_CURSO,NOMBRE_CURSO,ID_TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_PERIODO")] DocenteEducacionContinua docenteEducacionContinua)
        {
            if (ModelState.IsValid)
            {
                db.DocenteEducacionContinua.Add(docenteEducacionContinua);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(docenteEducacionContinua);
        }

        // GET: DocenteEducacionContinua/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteEducacionContinua docenteEducacionContinua = await db.DocenteEducacionContinua.FindAsync(id);
            if (docenteEducacionContinua == null)
            {
                return HttpNotFound();
            }
            return View(docenteEducacionContinua);
        }

        // POST: DocenteEducacionContinua/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_CURSO,NOMBRE_CURSO,ID_TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_PERIODO")] DocenteEducacionContinua docenteEducacionContinua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docenteEducacionContinua).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(docenteEducacionContinua);
        }

        // GET: DocenteEducacionContinua/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteEducacionContinua docenteEducacionContinua = await db.DocenteEducacionContinua.FindAsync(id);
            if (docenteEducacionContinua == null)
            {
                return HttpNotFound();
            }
            return View(docenteEducacionContinua);
        }

        // POST: DocenteEducacionContinua/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DocenteEducacionContinua docenteEducacionContinua = await db.DocenteEducacionContinua.FindAsync(id);
            db.DocenteEducacionContinua.Remove(docenteEducacionContinua);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.DocenteEducacionContinua.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
