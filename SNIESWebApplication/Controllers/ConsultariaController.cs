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

    public class ConsultariaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consultaria
        public async Task<ActionResult> Index()
        {
            return View(await db.Consultaria.ToListAsync());
        }

        // GET: Consultaria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultaria consultaria = await db.Consultaria.FindAsync(id);
            if (consultaria == null)
            {
                return HttpNotFound();
            }
            return View(consultaria);
        }

        // GET: Consultaria/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Consultaria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ID_IES,NOMBRE_IES,AÑO,SEMESTRE,COD_CONSULTORIA,DESC_CONSULTORIA,COD_CINE,CINE_CAMPO_DETALLADO,NOMBRE_INSTITUCION,COD_SECTOR,SECTOR,VALOR,FECHA_INICIO,_FECHA_FINAL,FECHA_PERIODO")] Consultaria consultaria)
        {
            if (ModelState.IsValid)
            {
                db.Consultaria.Add(consultaria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(consultaria);
        }

        // GET: Consultaria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultaria consultaria = await db.Consultaria.FindAsync(id);
            if (consultaria == null)
            {
                return HttpNotFound();
            }
            return View(consultaria);
        }

        // POST: Consultaria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ID_IES,NOMBRE_IES,AÑO,SEMESTRE,COD_CONSULTORIA,DESC_CONSULTORIA,COD_CINE,CINE_CAMPO_DETALLADO,NOMBRE_INSTITUCION,COD_SECTOR,SECTOR,VALOR,FECHA_INICIO,_FECHA_FINAL,FECHA_PERIODO")] Consultaria consultaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultaria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(consultaria);
        }

        // GET: Consultaria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultaria consultaria = await db.Consultaria.FindAsync(id);
            if (consultaria == null)
            {
                return HttpNotFound();
            }
            return View(consultaria);
        }

        // POST: Consultaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Consultaria consultaria = await db.Consultaria.FindAsync(id);
            db.Consultaria.Remove(consultaria);
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

            List<Consultaria> listaConsultaria = new List<Consultaria>();
            List<RecHumanoConsultoria> listaRecHumanoConsultoria = new List<RecHumanoConsultoria>();
            List<ActividadRecHumano> listaActividadRecHumano = new List<ActividadRecHumano>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaConsultaria.Add(
                            new Consultaria()
                            {
                                ID_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                AÑO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                COD_CONSULTORIA = matrixValorHoja[i, j = j + 1],
                                DESC_CONSULTORIA = matrixValorHoja[i, j = j + 1],
                                COD_CINE = matrixValorHoja[i, j = j + 1],
                                CINE_CAMPO_DETALLADO = matrixValorHoja[i, j = j + 1],
                                NOMBRE_INSTITUCION = matrixValorHoja[i, j = j + 1],
                                COD_SECTOR = matrixValorHoja[i, j = j + 1],
                                SECTOR = matrixValorHoja[i, j = j + 1],
                                VALOR = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                _FECHA_FINAL = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.Consultaria.AddRange(listaConsultaria);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaRecHumanoConsultoria.Add(
                            new RecHumanoConsultoria()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                AÑO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_CONSULTORIA = matrixValorHoja[i, j = j + 1],
                                DESCRIPCION_CONSULTORIA = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                NUMERO_DOCUMENTO = matrixValorHoja[i, j = j + 1],
                                PRIMER_NOMBRE = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_NOMBRE = matrixValorHoja[i, j = j + 1],
                                PRIMER_APELLIDO = matrixValorHoja[i, j = j + 1],
                                SEGUNDO_APELLIDO = matrixValorHoja[i, j = j + 1],
                                NIVEL_ESTUDIO = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.RecHumanoConsultoria.AddRange(listaRecHumanoConsultoria);
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
