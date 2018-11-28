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
    using System.IO;
    using OfficeOpenXml;

    public class DocenteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Docente
        public async Task<ActionResult> Index()
        {
            return View(await db.Docente.ToListAsync());
        }

        // GET: Docente/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = await db.Docente.FindAsync(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // GET: Docente/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Docente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_NACIMIENTO,PAIS_NACIMIENTO,MUNICIPIO_NACIMIENTO,NIVEL_MAXIMO_ESTUDIO,TITULO_RECIBIDO,FECHA_GRADO,PAIS_INSTITUCION_ESTUDIO,TITULO_CONVALIDADO,ID_IES_ESTUDIO,NOMBRE_INSTITUCION_ESTUDIO,METODOLOGIA_PROGRAMA,FECHA_INGRESO,FECHA_PERIODO")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                db.Docente.Add(docente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(docente);
        }

        // GET: Docente/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = await db.Docente.FindAsync(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // POST: Docente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,FECHA_NACIMIENTO,PAIS_NACIMIENTO,MUNICIPIO_NACIMIENTO,NIVEL_MAXIMO_ESTUDIO,TITULO_RECIBIDO,FECHA_GRADO,PAIS_INSTITUCION_ESTUDIO,TITULO_CONVALIDADO,ID_IES_ESTUDIO,NOMBRE_INSTITUCION_ESTUDIO,METODOLOGIA_PROGRAMA,FECHA_INGRESO,FECHA_PERIODO")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(docente);
        }

        // GET: Docente/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docente docente = await db.Docente.FindAsync(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // POST: Docente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Docente docente = await db.Docente.FindAsync(id);
            db.Docente.Remove(docente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CargaPlantillaExcel(HttpPostedFileBase plantillaCargaExcel, int PeriodoId)
        {
            if (plantillaCargaExcel != null && !string.IsNullOrEmpty(plantillaCargaExcel.FileName) && plantillaCargaExcel.ContentLength != 0)
            {
                if (plantillaCargaExcel.FileName.EndsWith("xls") || plantillaCargaExcel.FileName.EndsWith("xlsx") || plantillaCargaExcel.FileName.EndsWith("xlsm") || plantillaCargaExcel.FileName.EndsWith("csv"))
                {

                    List<ActividadBienestar> listaActividadBienestar = new List<ActividadBienestar>();
                    string fileName = plantillaCargaExcel.FileName;
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/PlantillaExcelSnies/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(fileName);

                    if (Directory.Exists(filePath))
                    {
                        Directory.Delete(filePath);
                    }

                    var _FECHA_PERIODO = db.Periodos.Where(x => x.Id == PeriodoId).FirstOrDefault();

                    if (plantillaCargaExcel.FileName.EndsWith("xls") || plantillaCargaExcel.FileName.EndsWith("xlsx") || plantillaCargaExcel.FileName.EndsWith("xlsm"))
                    {
                        using (var paquete = new ExcelPackage(plantillaCargaExcel.InputStream))
                        {
                            var hojaActuales = paquete.Workbook.Worksheets;
                            var cantidadHojas = hojaActuales.Count;

                            foreach (var hoja in hojaActuales)
                            {
                                var nroColumna = hoja.Dimension.End.Column;
                                var nroFila = hoja.Dimension.End.Row;
                                string[,] matrixValorHoja = new string[nroFila - 1, nroColumna];

                                for (int i = 2; i <= nroFila; i++)
                                {
                                    for (int j = 1; j <= nroColumna; j++)
                                    {
                                        matrixValorHoja[i - 2, j - 1] = (hoja.Cells[i, j].Value == null) ? string.Empty : hoja.Cells[i, j].Value.ToString();
                                    }
                                }
                                GuardarDatos(matrixValorHoja, hoja.Index, _FECHA_PERIODO.FechaPeriodo);
                            }
                        }
                        return View("Index", db.ActividadBienestar.ToList());
                    }
                    else
                    {
                        string extesion = Path.GetExtension(fileName);
                        plantillaCargaExcel.SaveAs(filePath);
                        string csvData = System.IO.File.ReadAllText(filePath);
                        int i = 0;

                        foreach (var row in csvData.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                if (i != 0)
                                {
                                }
                                i++;
                            }
                        }
                        db.ActividadBienestar.AddRange(listaActividadBienestar);
                        db.SaveChanges();
                        return View("Index", db.ActividadBienestar.ToList());
                    }
                }
                else
                {
                    ViewBag.CargaMasivaCatalogo = "Error! La plantilla no es un archivo Excel!";
                    return PartialView("Create");
                }
            }
            else
            {
                ViewBag.CargaMasivaCatalogo = "Error! no se ha cargado ningun archivo.";
                return PartialView("Create");
            }
        }

        private void GuardarDatos(string[,] matrixValorHoja, int index, string _FECHA_PERIODO)
        {
            var nroFila = matrixValorHoja.GetLength(0);

            List<Docente> listaDocente = new List<Docente>();
            List<CapacitacionDocente> listaCapacitacionDocente = new List<CapacitacionDocente>();
            List<ContratoDocente> listaContratoDocente = new List<ContratoDocente>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaDocente.Add(
                            new Docente()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                NUMERO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                PRIMER_NOMBRE = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_NOMBRE = matrixValorHoja[i, j = j + 1],
                                PRIMER_APELLIDO = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_APELLIDO = matrixValorHoja[i, j = j + 1],
                                FECHA_NACIMIENTO = matrixValorHoja[i, j = j + 1],
                                PAIS_NACIMIENTO = matrixValorHoja[i, j = j + 1],
                                MUNICIPIO_NACIMIENTO = matrixValorHoja[i, j = j + 1],
                                NIVEL_MAXIMO_ESTUDIO = matrixValorHoja[i, j = j + 1],
                                TITULO_RECIBIDO = matrixValorHoja[i, j = j + 1],
                                FECHA_GRADO = matrixValorHoja[i, j = j + 1],
                                PAIS_INSTITUCION_ESTUDIO = matrixValorHoja[i, j = j + 1],
                                TITULO_CONVALIDADO = matrixValorHoja[i, j = j + 1],
                                ID_IES_ESTUDIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_INSTITUCION_ESTUDIO = matrixValorHoja[i, j = j + 1],
                                METODOLOGIA_PROGRAMA = matrixValorHoja[i, j = j + 1],
                                FECHA_INGRESO = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.Docente.AddRange(listaDocente);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaCapacitacionDocente.Add(
                            new CapacitacionDocente()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                NUMERO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                PRIMER_NOMBRE = matrixValorHoja[i, j = j + 1],
                                PRIMER_APELLIDO = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_APELLIDO = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_CAPACITACION = matrixValorHoja[i, j = j + 1],
                                TIPO_CAPACITACION = matrixValorHoja[i, j = j + 1],
                                NO_HORAS_CURSADAS = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_CURSO = matrixValorHoja[i, j = j + 1],
                                TIPO_CURSO = matrixValorHoja[i, j = j + 1],
                                ID_TEMA_CURSO = matrixValorHoja[i, j = j + 1],
                                TEMA_CURSO = matrixValorHoja[i, j = j + 1],
                                ID_PAIS = matrixValorHoja[i, j = j + 1],
                                PAIS = matrixValorHoja[i, j = j + 1],
                                NOMBRE_CURSO = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.CapacitacionDocente.AddRange(listaCapacitacionDocente);
                            db.SaveChanges();
                        }
                        break;

                    case 3:
                        listaContratoDocente.Add(
                            new ContratoDocente()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                NUMERO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                PRIMER_NOMBRE = matrixValorHoja[i, j = j + 1],
                                PRIMER_APELLIDO = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_APELLIDO = matrixValorHoja[i, j = j + 1],
                                PORCENTAJE_DOCENCIA = matrixValorHoja[i, j = j + 1],
                                PORCENTAJE_INVESTIGACION = matrixValorHoja[i, j = j + 1],
                                PORCENTAJE_ADMINISTRATIVA = matrixValorHoja[i, j = j + 1],
                                PORCENTAJE_EXTENSION = matrixValorHoja[i, j = j + 1],
                                PORCENTAJE_OTRAS_ACTIVIDADES = matrixValorHoja[i, j = j + 1],
                                HORAS_DEDICACION_SEMESTRE = matrixValorHoja[i, j = j + 1],
                                DEDICACION = matrixValorHoja[i, j = j + 1],
                                TIPO_CONTRATO = matrixValorHoja[i, j = j + 1],
                                ASIGNACION_BASICA_MENSUAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ContratoDocente.AddRange(listaContratoDocente);
                            db.SaveChanges();
                        }
                        break;

                    default:
                        break;
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