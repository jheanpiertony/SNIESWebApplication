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
    using System.Reflection;
    using System.Dynamic;
    using System.Data.Entity.Core.Metadata.Edm;

    public class ActividadCulturalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActividadCultural
        public async Task<ActionResult> Index()
        {
            return View(await db.ActividadCultural.ToListAsync());
        }

        // GET: ActividadCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadCultural actividadCultural = await db.ActividadCultural.FindAsync(id);
            if (actividadCultural == null)
            {
                return HttpNotFound();
            }
            return View(actividadCultural);
        }

        // GET: ActividadCultural/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ActividadCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD_ORGANIZACIONAL,UNIDAD_ORGANIZACIONAL,CODIGO_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] ActividadCultural actividadCultural)
        {
            if (ModelState.IsValid)
            {
                db.ActividadCultural.Add(actividadCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(actividadCultural);
        }

        // GET: ActividadCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadCultural actividadCultural = await db.ActividadCultural.FindAsync(id);
            if (actividadCultural == null)
            {
                return HttpNotFound();
            }
            return View(actividadCultural);
        }

        // POST: ActividadCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD_ORGANIZACIONAL,UNIDAD_ORGANIZACIONAL,CODIGO_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] ActividadCultural actividadCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actividadCultural);
        }

        // GET: ActividadCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadCultural actividadCultural = await db.ActividadCultural.FindAsync(id);
            if (actividadCultural == null)
            {
                return HttpNotFound();
            }
            return View(actividadCultural);
        }

        // POST: ActividadCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActividadCultural actividadCultural = await db.ActividadCultural.FindAsync(id);
            db.ActividadCultural.Remove(actividadCultural);
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

            List<ActividadCultural> listaActividadCultural = new List<ActividadCultural>();
            List<RecHumanoCultural> listaRecHumanoCultural = new List<RecHumanoCultural>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaActividadCultural.Add(
                            new ActividadCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                COD_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                COD_TIPO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                TIPO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                FECHA_FINAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ActividadCultural.AddRange(listaActividadCultural);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaRecHumanoCultural.Add(
                            new RecHumanoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                COD_UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                UNIDAD_ORGANIZACIONAL = matrixValorHoja[i, j = j + 1],
                                CODIGO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                COD_TIPO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                TIPO_ACTIVIDAD = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                FECHA_FINAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.RecHumanoCultural.AddRange(listaRecHumanoCultural);
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
