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

    public class ServicioExtensionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServicioExtension
        public async Task<ActionResult> Index()
        {
            return View(await db.ServicioExtension.ToListAsync());
        }

        // GET: ServicioExtension/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioExtension servicioExtension = await db.ServicioExtension.FindAsync(id);
            if (servicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(servicioExtension);
        }

        // GET: ServicioExtension/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ServicioExtension/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,DESCRIPCION_SERVICIO,VALOR_SERVICIO,AREA_EXTENSION,FECHA_INICIO,FECHA_FINAL,NOMBRE_CONTACTO,APELLIDO_CONTACTO,TELEFONO_CONTACTO,EMAIL_CONTACTO,TIENE_COSTO,CRITERIO_ELEGIBILIDAD,FECHA_PERIODO")] ServicioExtension servicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.ServicioExtension.Add(servicioExtension);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(servicioExtension);
        }

        // GET: ServicioExtension/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioExtension servicioExtension = await db.ServicioExtension.FindAsync(id);
            if (servicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(servicioExtension);
        }

        // POST: ServicioExtension/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_UNIDAD_ORGANIZACIONAL,CODIGO_SERVICIO,NOMBRE_SERVICIO,DESCRIPCION_SERVICIO,VALOR_SERVICIO,AREA_EXTENSION,FECHA_INICIO,FECHA_FINAL,NOMBRE_CONTACTO,APELLIDO_CONTACTO,TELEFONO_CONTACTO,EMAIL_CONTACTO,TIENE_COSTO,CRITERIO_ELEGIBILIDAD,FECHA_PERIODO")] ServicioExtension servicioExtension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicioExtension).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servicioExtension);
        }

        // GET: ServicioExtension/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicioExtension servicioExtension = await db.ServicioExtension.FindAsync(id);
            if (servicioExtension == null)
            {
                return HttpNotFound();
            }
            return View(servicioExtension);
        }

        // POST: ServicioExtension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServicioExtension servicioExtension = await db.ServicioExtension.FindAsync(id);
            db.ServicioExtension.Remove(servicioExtension);
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

            List<ServicioExtension> listaServicioExtension = new List<ServicioExtension>();
            List<AreaTrabajoServicioExtension> listaAreaTrabajoServicioExtension = new List<AreaTrabajoServicioExtension>();
            List<CicloVitalServicioExtension> listaCicloVitalServicioExtension = new List<CicloVitalServicioExtension>();

            List<EntidadNacionalServicioExtension> listaEntidadNacionalServicioExtension = new List<EntidadNacionalServicioExtension>();
            List<FuenteInternacionalServicioExtension> listaFuenteInternacionalServicioExtension = new List<FuenteInternacionalServicioExtension>();
            List<FuenteNacionalServicioExtension> listaFuenteNacionalServicioExtension = new List<FuenteNacionalServicioExtension>();

            List<OtraEntidadServicioExtension> listaOtraEntidadServicioExtension = new List<OtraEntidadServicioExtension>();
            List<PoblacionCondicionalServicioExtension> listaPoblacionCondicionalServicioExtension = new List<PoblacionCondicionalServicioExtension>();
            List<PoblacionGrupoServicioExtension> listaPoblacionGrupoServicioExtension = new List<PoblacionGrupoServicioExtension>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaServicioExtension.Add(
                            new ServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                DESCRIPCION_SERVICIO = matrixValorHoja[i, j = j + 1],
                                VALOR_SERVICIO = matrixValorHoja[i, j = j + 1],
                                AREA_EXTENSION = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                FECHA_FINAL = matrixValorHoja[i, j = j + 1],
                                NOMBRE_CONTACTO = matrixValorHoja[i, j = j + 1],
                                APELLIDO_CONTACTO = matrixValorHoja[i, j = j + 1],
                                TELEFONO_CONTACTO = matrixValorHoja[i, j = j + 1],
                                EMAIL_CONTACTO = matrixValorHoja[i, j = j + 1],
                                TIENE_COSTO = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ServicioExtension.AddRange(listaServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaAreaTrabajoServicioExtension.Add(
                            new AreaTrabajoServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                ÁREA_TRABAJO = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.AreaTrabajoServicioExtension.AddRange(listaAreaTrabajoServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 3:
                        listaCicloVitalServicioExtension.Add(
                            new CicloVitalServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                CICLO_VITAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.CicloVitalServicioExtension.AddRange(listaCicloVitalServicioExtension);
                            db.SaveChanges();
                        }
                        break;
                    case 4:

                        listaEntidadNacionalServicioExtension.Add(
                            new EntidadNacionalServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                ENTIDAD_NACIONAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.EntidadNacionalServicioExtension.AddRange(listaEntidadNacionalServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 5:
                        listaFuenteInternacionalServicioExtension.Add(
                            new FuenteInternacionalServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_INSTITUCION = matrixValorHoja[i, j = j + 1],
                                PAIS = matrixValorHoja[i, j = j + 1],
                                FUENTE_INTERNACIONAL = matrixValorHoja[i, j = j + 1],
                                VALOR_FINANCIACION = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FuenteInternacionalServicioExtension.AddRange(listaFuenteInternacionalServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 6:
                        listaFuenteNacionalServicioExtension.Add(
                            new FuenteNacionalServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                AÑO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                FUENTE_NACIONAL = matrixValorHoja[i, j = j + 1],
                                VALOR_FINANCIACION = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FuenteNacionalServicioExtension.AddRange(listaFuenteNacionalServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 7:

                        listaOtraEntidadServicioExtension.Add(
                            new OtraEntidadServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_ENTIDAD = matrixValorHoja[i, j = j + 1],
                                PAIS = matrixValorHoja[i, j = j + 1],
                                SECTOR_ENTIDAD = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.OtraEntidadServicioExtension.AddRange(listaOtraEntidadServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 8:
                        listaPoblacionCondicionalServicioExtension.Add(
                            new PoblacionCondicionalServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                POBLACION_CONDICION = matrixValorHoja[i, j = j + 1],
                                CANTIDAD = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.PoblacionCondicionalServicioExtension.AddRange(listaPoblacionCondicionalServicioExtension);
                            db.SaveChanges();
                        }
                        break;

                    case 9:
                        listaPoblacionGrupoServicioExtension.Add(
                            new PoblacionGrupoServicioExtension()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_SERVICIO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_SERVICIO = matrixValorHoja[i, j = j + 1],
                                POBLACION_GRUPO = matrixValorHoja[i, j = j + 1],
                                CANTIDAD = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.PoblacionGrupoServicioExtension.AddRange(listaPoblacionGrupoServicioExtension);
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