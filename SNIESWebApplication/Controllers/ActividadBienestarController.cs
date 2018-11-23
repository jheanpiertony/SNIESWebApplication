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

    public class ActividadBienestarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActividadBienestar
        public async Task<ActionResult> Index()
        {
            return View(await db.ActividadBienestar.ToListAsync());
        }

        // GET: ActividadBienestar/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadBienestar actividadBienestar = await db.ActividadBienestar.FindAsync(id);
            if (actividadBienestar == null)
            {
                return HttpNotFound();
            }
            return View(actividadBienestar);
        }

        // GET: ActividadBienestar/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ActividadBienestar/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD,UNIDAD_ORGANIZACIONAL,COD_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] ActividadBienestar actividadBienestar)
        {
            if (ModelState.IsValid)
            {
                db.ActividadBienestar.Add(actividadBienestar);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(actividadBienestar);
        }

        // GET: ActividadBienestar/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadBienestar actividadBienestar = await db.ActividadBienestar.FindAsync(id);
            if (actividadBienestar == null)
            {
                return HttpNotFound();
            }
            return View(actividadBienestar);
        }

        // POST: ActividadBienestar/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_UNIDAD,UNIDAD_ORGANIZACIONAL,COD_ACTIVIDAD,ACTIVIDAD,COD_TIPO_ACTIVIDAD,TIPO_ACTIVIDAD,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] ActividadBienestar actividadBienestar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadBienestar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actividadBienestar);
        }

        // GET: ActividadBienestar/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadBienestar actividadBienestar = await db.ActividadBienestar.FindAsync(id);
            if (actividadBienestar == null)
            {
                return HttpNotFound();
            }
            return View(actividadBienestar);
        }

        // POST: ActividadBienestar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActividadBienestar actividadBienestar = await db.ActividadBienestar.FindAsync(id);
            db.ActividadBienestar.Remove(actividadBienestar);
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

                    var _FECHA_PERIODO = db.Periodos.Where(x => x.Id == PeriodoId).First();

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
                                string[,] matrixValorHoja = new string[nroFila-1, nroColumna];

                                for (int i = 2; i <= nroFila; i++)
                                {
                                    for (int j = 1; j <= nroColumna; j++)
                                    {
                                        matrixValorHoja[i - 2, j - 1] = (hoja.Cells[i, j].Value == null)? string.Empty: hoja.Cells[i, j].Value.ToString();
                                    }
                                }
                                GuardarDatos(matrixValorHoja, hoja.Index);
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

        private void GuardarDatos(string[,] matrixValorHoja, int index)
        {
            throw new NotImplementedException();
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
