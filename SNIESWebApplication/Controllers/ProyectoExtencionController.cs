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
    using System.IO;
    using OfficeOpenXml;

    public class ProyectoExtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProyectoExtencion
        public async Task<ActionResult> Index()
        {
            return View(await db.ProyectoExtencion.ToListAsync());
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANZIACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                DESCRIPCION_PROYECTO = matrixValorHoja[i, j = j + 1],
                                VALOR_PROYECTO = matrixValorHoja[i, j = j + 1],
                                ÁREA_EXTENSION = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                FECHA_FINAL = matrixValorHoja[i, j = j + 1],
                                NOMBRE_CONTACTO = matrixValorHoja[i, j = j + 1],
                                APELLIDO_CONTACTO = matrixValorHoja[i, j = j + 1],
                                TELEFONO_CONTACTO = matrixValorHoja[i, j = j + 1],
                                CORREO_CONTACTO = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                ÁREA_TRABAJO = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                CICLO_VITAL = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                ENTIDAD_NACIONAL = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_INSTITUCION = matrixValorHoja[i, j = j + 1],
                                PAIS = matrixValorHoja[i, j = j + 1],
                                FUENTE_INTERNACIONAL = matrixValorHoja[i, j = j + 1],
                                VALOR_FINANCIACION = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                FUENTE_NACIONAL = matrixValorHoja[i, j = j + 1],
                                VALOR_FINANCIACION = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_ENTIDAD = matrixValorHoja[i, j = j + 1],
                                PAIS = matrixValorHoja[i, j = j + 1],
                                SECTOR_ENTIDAD = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                POBLACION = matrixValorHoja[i, j = j + 1],
                                CANTIDAD = matrixValorHoja[i, j = j + 1],
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
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACINAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_PROYECTO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_PROYECTO = matrixValorHoja[i, j = j + 1],
                                POBLACION = matrixValorHoja[i, j = j + 1],
                                CANTIDAD = matrixValorHoja[i, j = j + 1],
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
