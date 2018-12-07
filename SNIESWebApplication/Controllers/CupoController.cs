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
    using SNIESWebApplication.Helpers;
    using ClosedXML.Excel;

    public class CupoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cupo
        public async Task<ActionResult> Index()
        {
            return View(await db.Cupos.OrderBy(x => new { x.FECHA_PERIODO, x.PROGRAM_NOMBRE }).ToListAsync());
        }

        // GET: Cupo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cupo cupo = await db.Cupos.FindAsync(id);
            if (cupo == null)
            {
                return HttpNotFound();
            }
            return View(cupo);
        }

        // GET: Cupo/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Cupo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,PRO_CONSECUTIVO,PROGRAM_NOMBRE,CODIGO_MUNICIPIO,NOMBRE_MUNICIPIO,CUPOS_NUEVOS_PROYECTADOS,CUPOS_TOTALES_PROYECTADOS,MATRICULA_TOTAL_ESPERADA,FUENTE,FECHA_PERIODO")] Cupo cupo)
        {
            if (ModelState.IsValid)
            {
                db.Cupos.Add(cupo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cupo);
        }

        // GET: Cupo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cupo cupo = await db.Cupos.FindAsync(id);
            if (cupo == null)
            {
                return HttpNotFound();
            }
            return View(cupo);
        }

        // POST: Cupo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,PRO_CONSECUTIVO,PROGRAM_NOMBRE,CODIGO_MUNICIPIO,NOMBRE_MUNICIPIO,CUPOS_NUEVOS_PROYECTADOS,CUPOS_TOTALES_PROYECTADOS,MATRICULA_TOTAL_ESPERADA,FUENTE,FECHA_PERIODO")] Cupo cupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cupo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cupo);
        }

        // GET: Cupo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cupo cupo = await db.Cupos.FindAsync(id);
            if (cupo == null)
            {
                return HttpNotFound();
            }
            return View(cupo);
        }

        // POST: Cupo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cupo cupo = await db.Cupos.FindAsync(id);
            db.Cupos.Remove(cupo);
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

                    List<Cupo> listaCupos = new List<Cupo>();
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
                        return View("Index", db.Cupos.ToList());
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
                        db.Cupos.AddRange(listaCupos);
                        db.SaveChanges();
                        return View("Index", db.Cupos.ToList());
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

            List<Cupo> listaCupo = new List<Cupo>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaCupo.Add(
                            new Cupo()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                PRO_CONSECUTIVO = matrixValorHoja[i, j++],
                                PROGRAM_NOMBRE = matrixValorHoja[i, j++],
                                CODIGO_MUNICIPIO = matrixValorHoja[i, j++],
                                NOMBRE_MUNICIPIO = matrixValorHoja[i, j++],
                                CUPOS_NUEVOS_PROYECTADOS = matrixValorHoja[i, j++],
                                CUPOS_TOTALES_PROYECTADOS = matrixValorHoja[i, j++],
                                MATRICULA_TOTAL_ESPERADA = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.Cupos.AddRange(listaCupo);
                            db.SaveChanges();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        
        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.Cupos.
                Select(x => new
                {
                    x.CODIGO_IES,
                    x.NOMBRE_IES,
                    x.ANO,
                    x.SEMESTRE,
                    x.PRO_CONSECUTIVO,
                    x.PROGRAM_NOMBRE,
                    x.CODIGO_MUNICIPIO,
                    x.NOMBRE_MUNICIPIO,
                    x.CUPOS_NUEVOS_PROYECTADOS,
                    x.CUPOS_TOTALES_PROYECTADOS,
                    x.MATRICULA_TOTAL_ESPERADA,
                    x.FECHA_PERIODO,
                    x.Id,

                }).ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "Cupos";

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
