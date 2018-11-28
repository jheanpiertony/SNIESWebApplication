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

    public class ConvenioInternacionalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConvenioInternacional
        public async Task<ActionResult> Index()
        {
            return View(await db.ConvenioInternacional.ToListAsync());
        }

        // GET: ConvenioInternacional/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacional convenioInternacional = await db.ConvenioInternacional.FindAsync(id);
            if (convenioInternacional == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacional);
        }

        // GET: ConvenioInternacional/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ConvenioInternacional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ANO,SEMESTRE,CODIGO_CONVENIO,ID_TIPO_CONVENIO,TIPO_CONVENIO,ACTIVIDAD_FORMACION,ACTIVIDAD_INVESTIGACION,ACTIVIDAD_EXTENSION,ACTIVIDAD_ADMINISTRATIVA,OTRAS_ACTIVIDADES_COOPERACION,FECHA_INICIO,FECHA_TERMINACION,FECHA_PERIODO")] ConvenioInternacional convenioInternacional)
        {
            if (ModelState.IsValid)
            {
                db.ConvenioInternacional.Add(convenioInternacional);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(convenioInternacional);
        }

        // GET: ConvenioInternacional/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacional convenioInternacional = await db.ConvenioInternacional.FindAsync(id);
            if (convenioInternacional == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacional);
        }

        // POST: ConvenioInternacional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ANO,SEMESTRE,CODIGO_CONVENIO,ID_TIPO_CONVENIO,TIPO_CONVENIO,ACTIVIDAD_FORMACION,ACTIVIDAD_INVESTIGACION,ACTIVIDAD_EXTENSION,ACTIVIDAD_ADMINISTRATIVA,OTRAS_ACTIVIDADES_COOPERACION,FECHA_INICIO,FECHA_TERMINACION,FECHA_PERIODO")] ConvenioInternacional convenioInternacional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(convenioInternacional).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(convenioInternacional);
        }

        // GET: ConvenioInternacional/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConvenioInternacional convenioInternacional = await db.ConvenioInternacional.FindAsync(id);
            if (convenioInternacional == null)
            {
                return HttpNotFound();
            }
            return View(convenioInternacional);
        }

        // POST: ConvenioInternacional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConvenioInternacional convenioInternacional = await db.ConvenioInternacional.FindAsync(id);
            db.ConvenioInternacional.Remove(convenioInternacional);
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

            List<ConvenioInternacional> listaConvenioInternacional = new List<ConvenioInternacional>();
            List<ConvenioInternacionalInstitucion> listaConvenioInternacionalInstitucion = new List<ConvenioInternacionalInstitucion>();

            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaConvenioInternacional.Add(
                            new ConvenioInternacional()
                            {
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_CONVENIO = matrixValorHoja[i, j = j + 1],
                                ID_TIPO_CONVENIO = matrixValorHoja[i, j = j + 1],
                                TIPO_CONVENIO = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD_FORMACION = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD_INVESTIGACION = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD_EXTENSION = matrixValorHoja[i, j = j + 1],
                                ACTIVIDAD_ADMINISTRATIVA = matrixValorHoja[i, j = j + 1],
                                OTRAS_ACTIVIDADES_COOPERACION = matrixValorHoja[i, j = j + 1],
                                FECHA_INICIO = matrixValorHoja[i, j = j + 1],
                                FECHA_TERMINACION = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ConvenioInternacional.AddRange(listaConvenioInternacional);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaConvenioInternacionalInstitucion.Add(
                            new ConvenioInternacionalInstitucion()
                            {
                                CODIGO_IES = matrixValorHoja[i, j = j + 1],
                                NOMBRE_IES = matrixValorHoja[i, j = j + 1],
                                ANO = matrixValorHoja[i, j = j + 1],
                                SEMESTRE = matrixValorHoja[i, j = j + 1],
                                CODIGO_CONVENIO = matrixValorHoja[i, j = j + 1],
                                INSTITUCION_ASOCIADA = matrixValorHoja[i, j = j + 1],
                                INSTITUCION_MULTILATERAL = matrixValorHoja[i, j = j + 1],
                                ID_PAIS_INSTITUCION_ASOCIADA = matrixValorHoja[i, j = j + 1],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ConvenioInternacionalInstitucion.AddRange(listaConvenioInternacionalInstitucion);
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
