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
    public class CicloVitalProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CicloVitalProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.CicloVitalProyectoExtencion.ToListAsync());
        }

        // GET: CicloVitalProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalProyectoExtencion cicloVitalProyectoExtencion = await db.CicloVitalProyectoExtencion.FindAsync(id);
            if (cicloVitalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalProyectoExtencion);
        }

        // GET: CicloVitalProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CicloVitalProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,CICLO_VITAL,FECHA_PERIODO")] CicloVitalProyectoExtencion cicloVitalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.CicloVitalProyectoExtencion.Add(cicloVitalProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cicloVitalProyectoExtencion);
        }

        // GET: CicloVitalProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalProyectoExtencion cicloVitalProyectoExtencion = await db.CicloVitalProyectoExtencion.FindAsync(id);
            if (cicloVitalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalProyectoExtencion);
        }

        // POST: CicloVitalProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,CICLO_VITAL,FECHA_PERIODO")] CicloVitalProyectoExtencion cicloVitalProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cicloVitalProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cicloVitalProyectoExtencion);
        }

        // GET: CicloVitalProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloVitalProyectoExtencion cicloVitalProyectoExtencion = await db.CicloVitalProyectoExtencion.FindAsync(id);
            if (cicloVitalProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(cicloVitalProyectoExtencion);
        }

        // POST: CicloVitalProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CicloVitalProyectoExtencion cicloVitalProyectoExtencion = await db.CicloVitalProyectoExtencion.FindAsync(id);
            db.CicloVitalProyectoExtencion.Remove(cicloVitalProyectoExtencion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.CicloVitalProyectoExtencion.ToList();
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
