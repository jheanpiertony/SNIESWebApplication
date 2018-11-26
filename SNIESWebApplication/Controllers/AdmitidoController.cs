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


    public class AdmitidoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admitido
        public async Task<ActionResult> Index()
        {
            return View(await db.Admitidos.ToListAsync());
        }

        // GET: Admitido/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admitido admitido = await db.Admitidos.FindAsync(id);
            if (admitido == null)
            {
                return HttpNotFound();
            }
            return View(admitido);
        }

        // GET: Admitido/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Admitido/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_DANE,DEPARTAMENTO,MUNICIPIO,PRO_CONSECUTIVO,PROGRAMA,TIPO_DOCUMENTO,NUMERO_DOCUMENTO")] Admitido admitido)
        {
            if (ModelState.IsValid)
            {
                db.Admitidos.Add(admitido);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(admitido);
        }

        // GET: Admitido/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admitido admitido = await db.Admitidos.FindAsync(id);
            if (admitido == null)
            {
                return HttpNotFound();
            }
            return View(admitido);
        }

        // POST: Admitido/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ID_IES,NOMBRE_IES,ANO,SEMESTRE,COD_DANE,DEPARTAMENTO,MUNICIPIO,PRO_CONSECUTIVO,PROGRAMA,TIPO_DOCUMENTO,NUMERO_DOCUMENTO")] Admitido admitido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admitido).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(admitido);
        }

        // GET: Admitido/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admitido admitido = await db.Admitidos.FindAsync(id);
            if (admitido == null)
            {
                return HttpNotFound();
            }
            return View(admitido);
        }

        // POST: Admitido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Admitido admitido = await db.Admitidos.FindAsync(id);
            db.Admitidos.Remove(admitido);
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

                    List<Admitido> listaAdmitidos = new List<Admitido>();
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
                                var ID_IES = string.IsNullOrEmpty(row.Split(';')[0]) ? string.Empty : row.Split(';')[0];
                                var NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1]) ? string.Empty : row.Split(';')[1];
                                var ANO = string.IsNullOrEmpty(row.Split(';')[2]) ? string.Empty : row.Split(';')[2];
                                var SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3]) ? string.Empty : row.Split(';')[3];
                                var COD_DANE = string.IsNullOrEmpty(row.Split(';')[4]) ? string.Empty : row.Split(';')[4];
                                var DEPARTAMENTO = string.IsNullOrEmpty(row.Split(';')[5]) ? string.Empty : row.Split(';')[5];
                                var MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[6]) ? string.Empty : row.Split(';')[6];
                                var PRO_CONSECUTIVO = string.IsNullOrEmpty(row.Split(';')[7]) ? string.Empty : row.Split(';')[7];
                                var PROGRAMA = string.IsNullOrEmpty(row.Split(';')[8]) ? string.Empty : row.Split(';')[8];
                                var TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[9]) ? string.Empty : row.Split(';')[9];
                                var NUMERO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[10]) ? string.Empty : row.Split(';')[10];

                                listaAdmitidos.Add(new Admitido()
                                {
                                    ID_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                    NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                    ANO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                    SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                    COD_DANE = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                    DEPARTAMENTO = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                    MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                    PRO_CONSECUTIVO = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                    PROGRAMA = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                    TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                    NUMERO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                    FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                });
                            }
                            i++;
                        }
                    }

                    db.Admitidos.AddRange(listaAdmitidos);
                    db.SaveChanges();
                    return View("Index", db.Admitidos.ToList());
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
