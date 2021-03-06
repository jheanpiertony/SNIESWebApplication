﻿namespace SNIESWebApplication.Controllers
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
    public class AreaTrabajoServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AreaTrabajoServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.AreaTrabajoServicioExtension.ToListAsync());
        }

        // GET: AreaTrabajoServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaTrabajoServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoServicioExtension areaTrabajoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.AreaTrabajoServicioExtension.Add(areaTrabajoServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // POST: AreaTrabajoServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,ÁREA_TRABAJO,FECHA_PERIODO")] AreaTrabajoServicioExtension areaTrabajoServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaTrabajoServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(areaTrabajoServicioExtension);
        }

        // GET: AreaTrabajoServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            if (areaTrabajoServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(areaTrabajoServicioExtension);
        }

        // POST: AreaTrabajoServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AreaTrabajoServicioExtension areaTrabajoServicioExtension = await db.AreaTrabajoServicioExtension.FindAsync(id);
            db.AreaTrabajoServicioExtension.Remove(areaTrabajoServicioExtension);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.AreaTrabajoServicioExtension.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "AreaTrabajoServicioExt";

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
