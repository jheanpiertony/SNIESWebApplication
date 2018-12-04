﻿using System;
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
    public class FuenteInternacionalServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FuenteInternacionalServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.FuenteInternacionalServicioExtension.ToListAsync());
        }

        // GET: FuenteInternacionalServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension = await db.FuenteInternacionalServicioExtension.FindAsync(id);
            if (fuenteInternacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(fuenteInternacionalServicioExtension);
        }

        // GET: FuenteInternacionalServicioExtension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FuenteInternacionalServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,NOMBRE_INSTITUCION,PAIS,FUENTE_INTERNACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.FuenteInternacionalServicioExtension.Add(fuenteInternacionalServicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fuenteInternacionalServicioExtension);
        }

        // GET: FuenteInternacionalServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension = await db.FuenteInternacionalServicioExtension.FindAsync(id);
            if (fuenteInternacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(fuenteInternacionalServicioExtension);
        }

        // POST: FuenteInternacionalServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,NOMBRE_INSTITUCION,PAIS,FUENTE_INTERNACIONAL,VALOR_FINANCIACION,FECHA_PERIODO")] FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fuenteInternacionalServicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fuenteInternacionalServicioExtension);
        }

        // GET: FuenteInternacionalServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension = await db.FuenteInternacionalServicioExtension.FindAsync(id);
            if (fuenteInternacionalServicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(fuenteInternacionalServicioExtension);
        }

        // POST: FuenteInternacionalServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FuenteInternacionalServicioExtension fuenteInternacionalServicioExtension = await db.FuenteInternacionalServicioExtension.FindAsync(id);
            db.FuenteInternacionalServicioExtension.Remove(fuenteInternacionalServicioExtension);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.FuenteInternacionalServicioExtension.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "FuenteInterServExt";

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
