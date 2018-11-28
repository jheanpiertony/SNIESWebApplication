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

namespace SNIESWebApplication.Controllers
{
    public class PoblacionGrupoProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoblacionGrupoProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.PoblacionGrupoProyectoExtencion.ToListAsync());
        }

        // GET: PoblacionGrupoProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion = await db.PoblacionGrupoProyectoExtencion.FindAsync(id);
            if (poblacionGrupoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoProyectoExtencion);
        }

        // GET: PoblacionGrupoProyectoExtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoblacionGrupoProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,POBLACION,CANTIDAD,FECHA_PERIODO")] PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.PoblacionGrupoProyectoExtencion.Add(poblacionGrupoProyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(poblacionGrupoProyectoExtencion);
        }

        // GET: PoblacionGrupoProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion = await db.PoblacionGrupoProyectoExtencion.FindAsync(id);
            if (poblacionGrupoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoProyectoExtencion);
        }

        // POST: PoblacionGrupoProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANIZACINAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,POBLACION,CANTIDAD,FECHA_PERIODO")] PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poblacionGrupoProyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(poblacionGrupoProyectoExtencion);
        }

        // GET: PoblacionGrupoProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion = await db.PoblacionGrupoProyectoExtencion.FindAsync(id);
            if (poblacionGrupoProyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(poblacionGrupoProyectoExtencion);
        }

        // POST: PoblacionGrupoProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PoblacionGrupoProyectoExtencion poblacionGrupoProyectoExtencion = await db.PoblacionGrupoProyectoExtencion.FindAsync(id);
            db.PoblacionGrupoProyectoExtencion.Remove(poblacionGrupoProyectoExtencion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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