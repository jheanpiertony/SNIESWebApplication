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
using SNIESWebApplication.Helpers;
using ClosedXML.Excel;

namespace SNIESWebApplication.Controllers
{
    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co,jgomezm@unicoc.edu.co")]
    public class EstudiantePrimerCursoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstudiantePrimerCurso
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "EstudiantePrimerCurso";
            var PeriodoIdActual = db.EstudiantesPrimerCurso.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.EstudiantesPrimerCurso.OrderBy(x => new { x.FECHA_PERIODO, x.NUMERO_DOCUMENTO }).ToListAsync());
        }

        // GET: EstudiantePrimerCurso/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantePrimerCurso estudiantePrimerCurso = await db.EstudiantesPrimerCurso.FindAsync(id);
            if (estudiantePrimerCurso == null)
            {
                return HttpNotFound();
            }
            return View(estudiantePrimerCurso);
        }

        // GET: EstudiantePrimerCurso/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: EstudiantePrimerCurso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,NOMBRE_PROGRAMA,ID_MUNICIPIO,DEPARTAMENTO,MUNICIPIO,ID_TIPO_VINCULACION,TIPO_VINCULACION,ID_GRUPO_ETNICO,GRUPO_ETNICO,ID_PUEBLO_INDIGENA,PUEBLO_INDIGENA,ID_COMINIDAD_NEGRA,COMUNIDAD_NEGRA,PERSONA_CON_DISCAPACIDAD,ID_TIPO_DISCAPACIDAD,ID_CAPACIDAD_EXCEP,CAPACIDAD_EXCEPCIONAL,COD_PRUEBA_SABER_11,FUENTE")] EstudiantePrimerCurso estudiantePrimerCurso)
        {
            if (ModelState.IsValid)
            {
                db.EstudiantesPrimerCurso.Add(estudiantePrimerCurso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(estudiantePrimerCurso);
        }

        // GET: EstudiantePrimerCurso/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantePrimerCurso estudiantePrimerCurso = await db.EstudiantesPrimerCurso.FindAsync(id);
            if (estudiantePrimerCurso == null)
            {
                return HttpNotFound();
            }
            return View(estudiantePrimerCurso);
        }

        // POST: EstudiantePrimerCurso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,NOMBRE_PROGRAMA,ID_MUNICIPIO,DEPARTAMENTO,MUNICIPIO,ID_TIPO_VINCULACION,TIPO_VINCULACION,ID_GRUPO_ETNICO,GRUPO_ETNICO,ID_PUEBLO_INDIGENA,PUEBLO_INDIGENA,ID_COMINIDAD_NEGRA,COMUNIDAD_NEGRA,PERSONA_CON_DISCAPACIDAD,ID_TIPO_DISCAPACIDAD,ID_CAPACIDAD_EXCEP,CAPACIDAD_EXCEPCIONAL,COD_PRUEBA_SABER_11,FUENTE")] EstudiantePrimerCurso estudiantePrimerCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiantePrimerCurso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estudiantePrimerCurso);
        }

        // GET: EstudiantePrimerCurso/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstudiantePrimerCurso estudiantePrimerCurso = await db.EstudiantesPrimerCurso.FindAsync(id);
            if (estudiantePrimerCurso == null)
            {
                return HttpNotFound();
            }
            return View(estudiantePrimerCurso);
        }

        // POST: EstudiantePrimerCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EstudiantePrimerCurso estudiantePrimerCurso = await db.EstudiantesPrimerCurso.FindAsync(id);
            db.EstudiantesPrimerCurso.Remove(estudiantePrimerCurso);
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

                    List<EstudiantePrimerCurso> listaEstudiantePrimerCurso = new List<EstudiantePrimerCurso>();
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

                                listaEstudiantePrimerCurso.Add(new EstudiantePrimerCurso()
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
                                    NOMBRE_PROGRAMA = string.IsNullOrEmpty(row.Split(';')[12].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[12].Replace("\"", string.Empty),
                                    ID_MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[13].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[13].Replace("\"", string.Empty),
                                    DEPARTAMENTO = string.IsNullOrEmpty(row.Split(';')[14].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[14].Replace("\"", string.Empty),
                                    MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[15].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[15].Replace("\"", string.Empty),
                                    ID_TIPO_VINCULACION = string.IsNullOrEmpty(row.Split(';')[16].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[16].Replace("\"", string.Empty),
                                    TIPO_VINCULACION = string.IsNullOrEmpty(row.Split(';')[17].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[17].Replace("\"", string.Empty),
                                    ID_GRUPO_ETNICO = string.IsNullOrEmpty(row.Split(';')[18].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[18].Replace("\"", string.Empty),
                                    GRUPO_ETNICO = string.IsNullOrEmpty(row.Split(';')[19].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[19].Replace("\"", string.Empty),
                                    ID_PUEBLO_INDIGENA = string.IsNullOrEmpty(row.Split(';')[20].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[20].Replace("\"", string.Empty),
                                    PUEBLO_INDIGENA = string.IsNullOrEmpty(row.Split(';')[21].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[21].Replace("\"", string.Empty),
                                    ID_COMINIDAD_NEGRA = string.IsNullOrEmpty(row.Split(';')[22].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[22].Replace("\"", string.Empty),
                                    COMUNIDAD_NEGRA = string.IsNullOrEmpty(row.Split(';')[23].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[23].Replace("\"", string.Empty),
                                    PERSONA_CON_DISCAPACIDAD = string.IsNullOrEmpty(row.Split(';')[24].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[24].Replace("\"", string.Empty),
                                    ID_TIPO_DISCAPACIDAD = string.IsNullOrEmpty(row.Split(';')[25].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[25].Replace("\"", string.Empty),
                                    ID_CAPACIDAD_EXCEP = string.IsNullOrEmpty(row.Split(';')[26].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[26].Replace("\"", string.Empty),
                                    CAPACIDAD_EXCEPCIONAL = string.IsNullOrEmpty(row.Split(';')[27].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[27].Replace("\"", string.Empty),
                                    COD_PRUEBA_SABER_11 = string.IsNullOrEmpty(row.Split(';')[28].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[28].Replace("\"", string.Empty),
                                    FUENTE = string.IsNullOrEmpty(row.Split(';')[29].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[29].Replace("\"", string.Empty),
                                    FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                });
                            }
                            i++;
                        }
                    }

                    db.EstudiantesPrimerCurso.AddRange(listaEstudiantePrimerCurso);
                    db.SaveChanges();
                    return RedirectToAction("Index");
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


        public void CrearPlantillaExcel(string PeriodoIdActual)
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.EstudiantesPrimerCurso.Where(x => x.FECHA_PERIODO == PeriodoIdActual).
                Select(x => new
                {
                    x.CODIGO_IES,
                    x.NOMBRE_IES,
                    x.ANO,
                    x.SEMESTRE,
                    x.ID_TIPO_DOCUMENTO,
                    x.TIPO_DOCUMENTO,
                    x.NUMERO_DOCUMENTO,
                    x.PRIMER_NOMBRE,
                    x.SEGUNDO_NOMBRE,
                    x.PRIMER_APELLIDO,
                    x.SEGUNDO_APELLIDO,
                    x.PROGRAMA_CONSECUTIVO,
                    x.NOMBRE_PROGRAMA,
                    x.ID_MUNICIPIO,
                    x.DEPARTAMENTO,
                    x.MUNICIPIO,
                    x.ID_TIPO_VINCULACION,
                    x.TIPO_VINCULACION,
                    x.ID_GRUPO_ETNICO,
                    x.GRUPO_ETNICO,
                    x.ID_PUEBLO_INDIGENA,
                    x.PUEBLO_INDIGENA,
                    x.ID_COMINIDAD_NEGRA,
                    x.COMUNIDAD_NEGRA,
                    x.PERSONA_CON_DISCAPACIDAD,
                    x.ID_TIPO_DISCAPACIDAD,
                    x.ID_CAPACIDAD_EXCEP,
                    x.CAPACIDAD_EXCEPCIONAL,
                    x.COD_PRUEBA_SABER_11,
                    x.FECHA_PERIODO,
                    x.Id,

                }).ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {
            
            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "EstudiantesPrimerCurso";

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
