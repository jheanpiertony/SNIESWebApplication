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
    public class EntidadNacionalProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntidadNacionalProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.EntidadNacionalProyectoExtencion.ToListAsync());
        }

        // GET: EntidadNacionalProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion = await db.EntidadNacionalProyectoExtencion.FindAsync(id);
            if (entidadNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalProyectoExtencion);
        }

        // GET: EntidadNacionalProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadNacionalProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,ENTIDAD_NACIONAL,FECHA_PERIODO")] EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.EntidadNacionalProyectoExtencion.Add(entidadNacionalProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entidadNacionalProyectoExtencion);
        }

        // GET: EntidadNacionalProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion = await db.EntidadNacionalProyectoExtencion.FindAsync(id);
            if (entidadNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalProyectoExtencion);
        }

        // POST: EntidadNacionalProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,ENTIDAD_NACIONAL,FECHA_PERIODO")] EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidadNacionalProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(entidadNacionalProyectoExtencion);
        }

        // GET: EntidadNacionalProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion = await db.EntidadNacionalProyectoExtencion.FindAsync(id);
            if (entidadNacionalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(entidadNacionalProyectoExtencion);
        }

        // POST: EntidadNacionalProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EntidadNacionalProyectoExtencion entidadNacionalProyectoExtencion = await db.EntidadNacionalProyectoExtencion.FindAsync(id);
            db.EntidadNacionalProyectoExtencion.Remove(entidadNacionalProyectoExtencion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.EntidadNacionalProyectoExtencion.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "EntidadNacionalProcExt";

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
