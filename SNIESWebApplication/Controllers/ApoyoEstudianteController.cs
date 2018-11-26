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

namespace SNIESWebApplication.Controllers
{
    public class ApoyoEstudianteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApoyoEstudiante
        public async Task<ActionResult> Index()
        {
            return View(await db.ApoyoEstudiantes.ToListAsync());
        }

        // GET: ApoyoEstudiante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApoyoEstudiante apoyoEstudiante = await db.ApoyoEstudiantes.FindAsync(id);
            if (apoyoEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(apoyoEstudiante);
        }

        // GET: ApoyoEstudiante/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ApoyoEstudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA_NOMBRE,ID_MUNICIPIO,DEPARTAMENTO,MUNICIPIO,RECIBIO_APOYO_FINANCIERO,RECIBIO_APOYO_ACADEMICO,RECIBIO_OTROS_APOYOS,FECHA_PERIODO")] ApoyoEstudiante apoyoEstudiante)
        {
            if (ModelState.IsValid)
            {
                db.ApoyoEstudiantes.Add(apoyoEstudiante);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(apoyoEstudiante);
        }

        // GET: ApoyoEstudiante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApoyoEstudiante apoyoEstudiante = await db.ApoyoEstudiantes.FindAsync(id);
            if (apoyoEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(apoyoEstudiante);
        }

        // POST: ApoyoEstudiante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA_NOMBRE,ID_MUNICIPIO,DEPARTAMENTO,MUNICIPIO,RECIBIO_APOYO_FINANCIERO,RECIBIO_APOYO_ACADEMICO,RECIBIO_OTROS_APOYOS,FECHA_PERIODO")] ApoyoEstudiante apoyoEstudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apoyoEstudiante).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(apoyoEstudiante);
        }

        // GET: ApoyoEstudiante/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApoyoEstudiante apoyoEstudiante = await db.ApoyoEstudiantes.FindAsync(id);
            if (apoyoEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(apoyoEstudiante);
        }

        // POST: ApoyoEstudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApoyoEstudiante apoyoEstudiante = await db.ApoyoEstudiantes.FindAsync(id);
            db.ApoyoEstudiantes.Remove(apoyoEstudiante);
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

                    List<ApoyoEstudiante> listaApoyoEstudiante = new List<ApoyoEstudiante>();
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

                    string extesion = Path.GetExtension(fileName);
                    plantillaCargaExcel.SaveAs(filePath);
                    string csvData = System.IO.File.ReadAllText(filePath);
                    int i = 0;
                    var _FECHA_PERIODO = db.Periodos.Where(x => x.Id == PeriodoId).FirstOrDefault();

                    foreach (var row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (i != 0)
                            {

                                listaApoyoEstudiante.Add(new ApoyoEstudiante()
                                {
                                    CODIGO_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                    NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                    ANO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                    SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                    ID_TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                    TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                    NUMERO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                    PRIMER_NOMBRE = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                    SEGUNDO_NOMBRE = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                    PRIMER_APELLIDO = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                    SEGUNDO_APELLIDO = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                    PROGRAMA_CONSECUTIVO = string.IsNullOrEmpty(row.Split(';')[11].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[11].Replace("\"", string.Empty),
                                    PROGRAMA_NOMBRE = string.IsNullOrEmpty(row.Split(';')[12].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[12].Replace("\"", string.Empty),
                                    ID_MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[13].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[13].Replace("\"", string.Empty),
                                    DEPARTAMENTO = string.IsNullOrEmpty(row.Split(';')[14].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[14].Replace("\"", string.Empty),
                                    MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[15].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[15].Replace("\"", string.Empty),
                                    RECIBIO_APOYO_FINANCIERO = string.IsNullOrEmpty(row.Split(';')[16].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[16].Replace("\"", string.Empty),
                                    RECIBIO_APOYO_ACADEMICO = string.IsNullOrEmpty(row.Split(';')[17].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[17].Replace("\"", string.Empty),
                                    RECIBIO_OTROS_APOYOS = string.IsNullOrEmpty(row.Split(';')[18].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[18].Replace("\"", string.Empty),
                                    FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                });
                            }
                            i++;
                        }
                    }

                    db.ApoyoEstudiantes.AddRange(listaApoyoEstudiante);
                    db.SaveChanges();
                    return View("Index", db.ApoyoEstudiantes.ToList());
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
