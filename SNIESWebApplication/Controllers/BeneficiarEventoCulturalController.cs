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
    public class BeneficiarEventoCulturalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BeneficiarEventoCultural
        public async Task<ActionResult> Index()
        {
            return View(await db.BeneficiarEventoCultural.ToListAsync());
        }

        // GET: BeneficiarEventoCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiarEventoCultural beneficiarEventoCultural = await db.BeneficiarEventoCultural.FindAsync(id);
            if (beneficiarEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(beneficiarEventoCultural);
        }

        // GET: BeneficiarEventoCultural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeneficiarEventoCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,ID_TIPO_BENEFICIARIO,TIPO_BENEFICIARIO,TOTAL_ASISTENTES,VALOR_ENTRADA,FECHA_PERIODO")] BeneficiarEventoCultural beneficiarEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.BeneficiarEventoCultural.Add(beneficiarEventoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(beneficiarEventoCultural);
        }

        // GET: BeneficiarEventoCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiarEventoCultural beneficiarEventoCultural = await db.BeneficiarEventoCultural.FindAsync(id);
            if (beneficiarEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(beneficiarEventoCultural);
        }

        // POST: BeneficiarEventoCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,ID_TIPO_BENEFICIARIO,TIPO_BENEFICIARIO,TOTAL_ASISTENTES,VALOR_ENTRADA,FECHA_PERIODO")] BeneficiarEventoCultural beneficiarEventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiarEventoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(beneficiarEventoCultural);
        }

        // GET: BeneficiarEventoCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiarEventoCultural beneficiarEventoCultural = await db.BeneficiarEventoCultural.FindAsync(id);
            if (beneficiarEventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(beneficiarEventoCultural);
        }

        // POST: BeneficiarEventoCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BeneficiarEventoCultural beneficiarEventoCultural = await db.BeneficiarEventoCultural.FindAsync(id);
            db.BeneficiarEventoCultural.Remove(beneficiarEventoCultural);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.BeneficiarEventoCultural.ToList();
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
