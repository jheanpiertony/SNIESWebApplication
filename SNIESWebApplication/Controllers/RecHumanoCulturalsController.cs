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
    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co")]
    public class RecHumanoCulturalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecHumanoCulturals
        public async Task<ActionResult> Index()
        {
            return View(await db.RecHumanoCultural.ToListAsync());
        }

        // GET: RecHumanoCulturals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoCultural recHumanoCultural = await db.RecHumanoCultural.FindAsync(id);
            if (recHumanoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoCultural);
        }

        // GET: RecHumanoCulturals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecHumanoCulturals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD_ORGANIZACIONAL,UNIDAD_ORGANIZACIONAL,CODIGO_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] RecHumanoCultural recHumanoCultural)
        {
            if (ModelState.IsValid)
            {
                db.RecHumanoCultural.Add(recHumanoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recHumanoCultural);
        }

        // GET: RecHumanoCulturals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoCultural recHumanoCultural = await db.RecHumanoCultural.FindAsync(id);
            if (recHumanoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoCultural);
        }

        // POST: RecHumanoCulturals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD_ORGANIZACIONAL,UNIDAD_ORGANIZACIONAL,CODIGO_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] RecHumanoCultural recHumanoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recHumanoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recHumanoCultural);
        }

        // GET: RecHumanoCulturals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecHumanoCultural recHumanoCultural = await db.RecHumanoCultural.FindAsync(id);
            if (recHumanoCultural == null)
            {
                return HttpNotFound();
            }
            return View(recHumanoCultural);
        }

        // POST: RecHumanoCulturals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecHumanoCultural recHumanoCultural = await db.RecHumanoCultural.FindAsync(id);
            db.RecHumanoCultural.Remove(recHumanoCultural);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.RecHumanoCultural.ToList();
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
