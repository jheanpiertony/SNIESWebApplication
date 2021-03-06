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
    using SNIESWebApplication.Helpers;
    using ClosedXML.Excel;

    [Authorize(Roles = "Administrador, Desarrollador, Calidad")]
    public class ProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProyectoExtencion
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "ProyectoExtencion";
            var PeriodoIdActual = db.ProyectoExtencion.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
            int i = 0;
            var listaPeriodo = new List<Periodo>();
            foreach (var item in PeriodoIdActual)
            {
                if (item.Key != null)
                {
                    listaPeriodo.Add(new Periodo() { Id = i++, FechaPeriodo = item.Key.ToString() });
                }
            }
            ViewBag.PeriodoIdActual = new SelectList(listaPeriodo, "Id", "FechaPeriodo");
            return View(await db.ProyectoExtencion.OrderBy(x => new { x.FECHA_PERIODO, x.CODIGO_PROYECTO }).ToListAsync());
        }

        // GET: ProyectoExtencion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProyectoExtencion proyectoExtencion = await db.ProyectoExtencion.FindAsync(id);
            if (proyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(proyectoExtencion);
        }

        // GET: ProyectoExtencion/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ProyectoExtencion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANZIACIONAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,DESCRIPCION_PROYECTO,VALOR_PROYECTO,ÁREA_EXTENSION,FECHA_INICIO,FECHA_FINAL,NOMBRE_CONTACTO,APELLIDO_CONTACTO,TELEFONO_CONTACTO,CORREO_CONTACTO,FECHA_PERIODO")] ProyectoExtencion proyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.ProyectoExtencion.Add(proyectoExtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(proyectoExtencion);
        }

        // GET: ProyectoExtencion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProyectoExtencion proyectoExtencion = await db.ProyectoExtencion.FindAsync(id);
            if (proyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(proyectoExtencion);
        }

        // POST: ProyectoExtencion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,UNIDAD_ORGANZIACIONAL,CODIGO_PROYECTO,NOMBRE_PROYECTO,DESCRIPCION_PROYECTO,VALOR_PROYECTO,ÁREA_EXTENSION,FECHA_INICIO,FECHA_FINAL,NOMBRE_CONTACTO,APELLIDO_CONTACTO,TELEFONO_CONTACTO,CORREO_CONTACTO,FECHA_PERIODO")] ProyectoExtencion proyectoExtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectoExtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(proyectoExtencion);
        }

        // GET: ProyectoExtencion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProyectoExtencion proyectoExtencion = await db.ProyectoExtencion.FindAsync(id);
            if (proyectoExtencion == null)
            {
                return HttpNotFound();
            }
            return View(proyectoExtencion);
        }

        // POST: ProyectoExtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProyectoExtencion proyectoExtencion = await db.ProyectoExtencion.FindAsync(id);
            db.ProyectoExtencion.Remove(proyectoExtencion);
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

                    List<ProyectoExtencion> listaProyectoExtencion = new List<ProyectoExtencion>();
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
                        return View("Index", db.ProyectoExtencion.ToList());
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
                        db.ProyectoExtencion.AddRange(listaProyectoExtencion);
                        db.SaveChanges();
                        return View("Index", db.ProyectoExtencion.ToList());
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

            List<ProyectoExtencion> listaProyectoExtencion = new List<ProyectoExtencion>();
            List<AreaTrabajoProyectoExtencion> listaAreaTrabajoProyectoExtencion = new List<AreaTrabajoProyectoExtencion>();
            List<CicloVitalProyectoExtencion> listaCicloVitalProyectoExtencion = new List<CicloVitalProyectoExtencion>();

            List<EntidadNacionalProyectoExtencion> listaEntidadNacionalProyectoExtencion = new List<EntidadNacionalProyectoExtencion>();
            List<FuenteInternacionalProyectoExtencion> listaFuenteInternacionalProyectoExtencion = new List<FuenteInternacionalProyectoExtencion>();
            List<FuenteNacionalProyectoExtencion> listaFuenteNacionalProyectoExtencion = new List<FuenteNacionalProyectoExtencion>();

            List<OtraEntidadProyectoExtencion> listaOtraEntidadProyectoExtencion = new List<OtraEntidadProyectoExtencion>();
            List<PoblacionCondiProyectoExtencion> listaPoblacionCondiProyectoExtencion = new List<PoblacionCondiProyectoExtencion>();
            List<PoblacionGrupoProyectoExtencion> listaPoblacionGrupoProyectoExtencion = new List<PoblacionGrupoProyectoExtencion>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaProyectoExtencion.Add(
                            new ProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANZIACIONAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                DESCRIPCION_PROYECTO = matrixValorHoja[i, j++],
                                VALOR_PROYECTO = matrixValorHoja[i, j++],
                                ÁREA_EXTENSION = matrixValorHoja[i, j++],
                                FECHA_INICIO = matrixValorHoja[i, j++],
                                FECHA_FINAL = matrixValorHoja[i, j++],
                                NOMBRE_CONTACTO = matrixValorHoja[i, j++],
                                APELLIDO_CONTACTO = matrixValorHoja[i, j++],
                                TELEFONO_CONTACTO = matrixValorHoja[i, j++],
                                CORREO_CONTACTO = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ProyectoExtencion.AddRange(listaProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaAreaTrabajoProyectoExtencion.Add(
                            new AreaTrabajoProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                ÁREA_TRABAJO = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.AreaTrabajoProyectoExtencion.AddRange(listaAreaTrabajoProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 3:
                        listaCicloVitalProyectoExtencion.Add(
                            new CicloVitalProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                CICLO_VITAL = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.CicloVitalProyectoExtencion.AddRange(listaCicloVitalProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;
                    case 4:

                        listaEntidadNacionalProyectoExtencion.Add(
                            new EntidadNacionalProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                ENTIDAD_NACIONAL = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.EntidadNacionalProyectoExtencion.AddRange(listaEntidadNacionalProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 5:
                        listaFuenteInternacionalProyectoExtencion.Add(
                            new FuenteInternacionalProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_INSTITUCION = matrixValorHoja[i, j++],
                                PAIS = matrixValorHoja[i, j++],
                                FUENTE_INTERNACIONAL = matrixValorHoja[i, j++],
                                VALOR_FINANCIACION = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FuenteInternacionalProyectoExtencion.AddRange(listaFuenteInternacionalProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 6:
                        listaFuenteNacionalProyectoExtencion.Add(
                            new FuenteNacionalProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                FUENTE_NACIONAL = matrixValorHoja[i, j++],
                                VALOR_FINANCIACION = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FuenteNacionalProyectoExtencion.AddRange(listaFuenteNacionalProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 7:

                        listaOtraEntidadProyectoExtencion.Add(
                            new OtraEntidadProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_ENTIDAD = matrixValorHoja[i, j++],
                                PAIS = matrixValorHoja[i, j++],
                                SECTOR_ENTIDAD = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.OtraEntidadProyectoExtencion.AddRange(listaOtraEntidadProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 8:
                        listaPoblacionCondiProyectoExtencion.Add(
                            new PoblacionCondiProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                POBLACION = matrixValorHoja[i, j++],
                                CANTIDAD = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.PoblacionCondiProyectoExtencion.AddRange(listaPoblacionCondiProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    case 9:
                        listaPoblacionGrupoProyectoExtencion.Add(
                            new PoblacionGrupoProyectoExtencion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j++],
                                CODIGO_PROYECTO = matrixValorHoja[i, j++],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j++],
                                POBLACION = matrixValorHoja[i, j++],
                                CANTIDAD = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.PoblacionGrupoProyectoExtencion.AddRange(listaPoblacionGrupoProyectoExtencion);
                            db.SaveChanges();
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();
            var lista = db.ProyectoExtencion.Where(x => x.FECHA_PERIODO == PeriodoIdActual).ToList();
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
