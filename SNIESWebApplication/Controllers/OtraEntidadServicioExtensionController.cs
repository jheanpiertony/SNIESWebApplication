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
    public class OtraEntidadServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OtraEntidadServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.OtraEntidadServicioExtension.ToListAsync());
        }

        // GET: OtraEntidadServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadServicioExtension otraEntidadServicioExtension = await db.OtraEntidadServicioExtension.FindAsync(id);
            if (otraEntidadServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadServicioExtension);
        }

        // GET: OtraEntidadServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtraEntidadServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,NOMBRE_ENTIDAD,PAIS,SECTOR_ENTIDAD,FECHA_PERIODO")] OtraEntidadServicioExtension otraEntidadServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.OtraEntidadServicioExtension.Add(otraEntidadServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(otraEntidadServicioExtension);
        }

        // GET: OtraEntidadServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadServicioExtension otraEntidadServicioExtension = await db.OtraEntidadServicioExtension.FindAsync(id);
            if (otraEntidadServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadServicioExtension);
        }

        // POST: OtraEntidadServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,NOMBRE_ENTIDAD,PAIS,SECTOR_ENTIDAD,FECHA_PERIODO")] OtraEntidadServicioExtension otraEntidadServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otraEntidadServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(otraEntidadServicioExtension);
        }

        // GET: OtraEntidadServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtraEntidadServicioExtension otraEntidadServicioExtension = await db.OtraEntidadServicioExtension.FindAsync(id);
            if (otraEntidadServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(otraEntidadServicioExtension);
        }

        // POST: OtraEntidadServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtraEntidadServicioExtension otraEntidadServicioExtension = await db.OtraEntidadServicioExtension.FindAsync(id);
            db.OtraEntidadServicioExtension.Remove(otraEntidadServicioExtension);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.OtraEntidadServicioExtension.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "OtraEntidadServicioExt";

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
