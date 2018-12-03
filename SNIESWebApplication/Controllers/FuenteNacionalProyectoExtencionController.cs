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
using System.IO;
using ClosedXML.Excel;
using SNIESWebApplication.Helpers;

namespace SNIESWebApplication.Controllers
{
    public class FuenteNacionalProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FuenteNacionalProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.FuenteNacionalProyectoExtencion.ToListAsync());
        }

        // GET: FuenteNacionalProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion = await db.FuenteNacionalProyectoExtencion.FindAsync(id);
            if (fuenteNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(fuenteNacionalProyectoExtencion);
        }

        // GET: FuenteNacionalProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FuenteNacionalProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,FUENTE_NACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.FuenteNacionalProyectoExtencion.Add(fuenteNacionalProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fuenteNacionalProyectoExtencion);
        }

        // GET: FuenteNacionalProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion = await db.FuenteNacionalProyectoExtencion.FindAsync(id);
            if (fuenteNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(fuenteNacionalProyectoExtencion);
        }

        // POST: FuenteNacionalProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,FUENTE_NACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fuenteNacionalProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fuenteNacionalProyectoExtencion);
        }

        // GET: FuenteNacionalProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion = await db.FuenteNacionalProyectoExtencion.FindAsync(id);
            if (fuenteNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(fuenteNacionalProyectoExtencion);
        }

        // POST: FuenteNacionalProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FuenteNacionalProyectoExtencion fuenteNacionalProyectoExtencion = await db.FuenteNacionalProyectoExtencion.FindAsync(id);
            db.FuenteNacionalProyectoExtencion.Remove(fuenteNacionalProyectoExtencion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.FuenteNacionalProyectoExtencion.ToList();
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
