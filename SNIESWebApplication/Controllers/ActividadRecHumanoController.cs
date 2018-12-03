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

    public class ActividadRecHumanoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActividadRecHumano
        public async Task<ActionResult> Index()
        {
            return View(await db.ActividadRecHumano.ToListAsync());
        }

        // GET: ActividadRecHumano/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadRecHumano actividadRecHumano = await db.ActividadRecHumano.FindAsync(id);
            if (actividadRecHumano == null)
            {
                return HttpNotFound();
            }
            return View(actividadRecHumano);
        }

        // GET: ActividadRecHumano/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActividadRecHumano/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD,UNIDAD_ORGANIZACIONAL,COD_ACTIVIDAD,ACTIVIDAD,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_PERIODO")] ActividadRecHumano actividadRecHumano)
        {
            if (ModelState.IsValid)
            {
                db.ActividadRecHumano.Add(actividadRecHumano);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(actividadRecHumano);
        }

        // GET: ActividadRecHumano/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadRecHumano actividadRecHumano = await db.ActividadRecHumano.FindAsync(id);
            if (actividadRecHumano == null)
            {
                return HttpNotFound();
            }
            return View(actividadRecHumano);
        }

        // POST: ActividadRecHumano/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD,UNIDAD_ORGANIZACIONAL,COD_ACTIVIDAD,ACTIVIDAD,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_PERIODO")] ActividadRecHumano actividadRecHumano)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadRecHumano).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actividadRecHumano);
        }

        // GET: ActividadRecHumano/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadRecHumano actividadRecHumano = await db.ActividadRecHumano.FindAsync(id);
            if (actividadRecHumano == null)
            {
                return HttpNotFound();
            }
            return View(actividadRecHumano);
        }

        // POST: ActividadRecHumano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActividadRecHumano actividadRecHumano = await db.ActividadRecHumano.FindAsync(id);
            db.ActividadRecHumano.Remove(actividadRecHumano);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.ActividadRecHumano.ToList();
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
