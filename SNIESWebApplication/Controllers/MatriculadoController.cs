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
    using SNIESWebApplication.Helpers;
    using ClosedXML.Excel;

    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co")]
    public class MatriculadoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Matriculado
        public async Task<ActionResult> Index()
        {
            ViewBag.Contolador = "Matriculado";
            var PeriodoIdActual = db.Matriculados.Select(x => new { x.FECHA_PERIODO }).GroupBy(x => x.FECHA_PERIODO).ToList();
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
            return View(await db.Matriculados.OrderBy(x => new { x.FECHA_PERIODO, x.NUMERO_DOCUMENTO }).ToListAsync());
        }

        // GET: Matriculado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculado matriculado = await db.Matriculados.FindAsync(id);
            if (matriculado == null)
            {
                return HttpNotFound();
            }
            return View(matriculado);
        }

        // GET: Matriculado/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Matriculado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,CODIGO_ESTUDIANTE,SEXO_BIOLOGICO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA,COD_DANE,DEPARTAMENTO,MUNICIPIO,FECHA_NACIMIENTO,ID_PAIS,PAIS,COD_DANE_NACIMIENTO,DEPARTAMENTO_NACIMIENTO,MUNICIPIO_NACIMIENTO,ID_ZONA_RESIDENCIA,ZONA_RESIDENCIA,NUMERO_MATERIAS_INSCRITAS,NUMERO_MATERIAS_APROBADAS,ES_REINTEGRO_ESTD_ANTES_DE1998,ANO_PRIMER_CURSO,SEMESTRE_PRIMER_CURSO,FUENTE,FECHA_PERIODO")] Matriculado matriculado)
        {
            if (ModelState.IsValid)
            {
                db.Matriculados.Add(matriculado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(matriculado);
        }

        // GET: Matriculado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculado matriculado = await db.Matriculados.FindAsync(id);
            if (matriculado == null)
            {
                return HttpNotFound();
            }
            return View(matriculado);
        }

        // POST: Matriculado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,CODIGO_ESTUDIANTE,SEXO_BIOLOGICO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PROGRAMA_CONSECUTIVO,PROGRAMA,COD_DANE,DEPARTAMENTO,MUNICIPIO,FECHA_NACIMIENTO,ID_PAIS,PAIS,COD_DANE_NACIMIENTO,DEPARTAMENTO_NACIMIENTO,MUNICIPIO_NACIMIENTO,ID_ZONA_RESIDENCIA,ZONA_RESIDENCIA,NUMERO_MATERIAS_INSCRITAS,NUMERO_MATERIAS_APROBADAS,ES_REINTEGRO_ESTD_ANTES_DE1998,ANO_PRIMER_CURSO,SEMESTRE_PRIMER_CURSO,FUENTE,FECHA_PERIODO")] Matriculado matriculado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matriculado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(matriculado);
        }

        // GET: Matriculado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculado matriculado = await db.Matriculados.FindAsync(id);
            if (matriculado == null)
            {
                return HttpNotFound();
            }
            return View(matriculado);
        }

        // POST: Matriculado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Matriculado matriculado = await db.Matriculados.FindAsync(id);
            db.Matriculados.Remove(matriculado);
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

                    List<Matriculado> listaMatriculado = new List<Matriculado>();
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

                                listaMatriculado.Add(new Matriculado()
                                {
                                    CODIGO_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                    NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                    ANO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                    SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                    ID_TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                    TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                    NUMERO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                    CODIGO_ESTUDIANTE = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                    SEXO_BIOLOGICO = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                    PRIMER_NOMBRE = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                    SEGUNDO_NOMBRE = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                    PRIMER_APELLIDO = string.IsNullOrEmpty(row.Split(';')[11].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[11].Replace("\"", string.Empty),
                                    SEGUNDO_APELLIDO = string.IsNullOrEmpty(row.Split(';')[12].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[12].Replace("\"", string.Empty),
                                    PROGRAMA_CONSECUTIVO = string.IsNullOrEmpty(row.Split(';')[13].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[13].Replace("\"", string.Empty),
                                    PROGRAMA = string.IsNullOrEmpty(row.Split(';')[14].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[14].Replace("\"", string.Empty),
                                    COD_DANE = string.IsNullOrEmpty(row.Split(';')[15].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[15].Replace("\"", string.Empty),
                                    DEPARTAMENTO = string.IsNullOrEmpty(row.Split(';')[16].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[16].Replace("\"", string.Empty),
                                    MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[17].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[17].Replace("\"", string.Empty),
                                    FECHA_NACIMIENTO = string.IsNullOrEmpty(row.Split(';')[18].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[18].Replace("\"", string.Empty),
                                    ID_PAIS = string.IsNullOrEmpty(row.Split(';')[19].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[19].Replace("\"", string.Empty),
                                    PAIS = string.IsNullOrEmpty(row.Split(';')[20].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[20].Replace("\"", string.Empty),
                                    COD_DANE_NACIMIENTO = string.IsNullOrEmpty(row.Split(';')[21].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[21].Replace("\"", string.Empty),
                                    DEPARTAMENTO_NACIMIENTO = string.IsNullOrEmpty(row.Split(';')[22].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[22].Replace("\"", string.Empty),
                                    MUNICIPIO_NACIMIENTO = string.IsNullOrEmpty(row.Split(';')[23].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[23].Replace("\"", string.Empty),
                                    ID_ZONA_RESIDENCIA = string.IsNullOrEmpty(row.Split(';')[24].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[24].Replace("\"", string.Empty),
                                    ZONA_RESIDENCIA = string.IsNullOrEmpty(row.Split(';')[25].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[25].Replace("\"", string.Empty),
                                    NUMERO_MATERIAS_INSCRITAS = string.IsNullOrEmpty(row.Split(';')[26].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[26].Replace("\"", string.Empty),
                                    NUMERO_MATERIAS_APROBADAS = string.IsNullOrEmpty(row.Split(';')[27].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[27].Replace("\"", string.Empty),
                                    ES_REINTEGRO_ESTD_ANTES_DE1998 = string.IsNullOrEmpty(row.Split(';')[28].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[28].Replace("\"", string.Empty),
                                    ANO_PRIMER_CURSO = string.IsNullOrEmpty(row.Split(';')[29].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[29].Replace("\"", string.Empty),
                                    SEMESTRE_PRIMER_CURSO = string.IsNullOrEmpty(row.Split(';')[30].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[30].Replace("\"", string.Empty),
                                    FUENTE = string.IsNullOrEmpty(row.Split(';')[31].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[31].Replace("\"", string.Empty),
                                    FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                });
                            }
                            i++;
                        }
                    }

                    db.Matriculados.AddRange(listaMatriculado);
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

            var lista = db.Matriculados.Where(x => x.FECHA_PERIODO == PeriodoIdActual).Select(x => 
                new {
                    x.CODIGO_IES,
                    x.NOMBRE_IES,
                    x.ANO,
                    x.SEMESTRE,
                    x.ID_TIPO_DOCUMENTO,
                    x.TIPO_DOCUMENTO,
                    x.NUMERO_DOCUMENTO,
                    x.CODIGO_ESTUDIANTE,
                    x.SEXO_BIOLOGICO,
                    x.PRIMER_NOMBRE,
                    x.SEGUNDO_NOMBRE,
                    x.PRIMER_APELLIDO,
                    x.SEGUNDO_APELLIDO,
                    x.PROGRAMA_CONSECUTIVO,
                    x.PROGRAMA,
                    x.COD_DANE,
                    x.DEPARTAMENTO,
                    x.MUNICIPIO,
                    x.FECHA_NACIMIENTO,
                    x.ID_PAIS,
                    x.PAIS,
                    x.COD_DANE_NACIMIENTO,
                    x.DEPARTAMENTO_NACIMIENTO,
                    x.MUNICIPIO_NACIMIENTO,
                    x.ID_ZONA_RESIDENCIA,
                    x.ZONA_RESIDENCIA,
                    x.NUMERO_MATERIAS_INSCRITAS,
                    x.NUMERO_MATERIAS_APROBADAS,
                    x.ES_REINTEGRO_ESTD_ANTES_DE1998,
                    x.ANO_PRIMER_CURSO,
                    x.SEMESTRE_PRIMER_CURSO,
                    x.FECHA_PERIODO,
                    x.Id,
                }).ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {
            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "Matriculados";

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
