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

namespace SNIESWebApplication.Controllers
{
    public class ProgramaPresencialeExteriorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProgramaPresencialeExterior
        public async Task<ActionResult> Index()
        {
            return View(await db.ProgramaPresencialeExterior.ToListAsync());
        }

        // GET: ProgramaPresencialeExterior/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaPresencialeExterior programaPresencialeExterior = await db.ProgramaPresencialeExterior.FindAsync(id);
            if (programaPresencialeExterior == null)
            {
                return HttpNotFound();
            }
            return View(programaPresencialeExterior);
        }

        // GET: ProgramaPresencialeExterior/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: ProgramaPresencialeExterior/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_PROGRAMA,PROGRAMA,CINE_CAMPO_DETALLADO,PAIS,MODALIDAD,FECHA_PERIODO")] ProgramaPresencialeExterior programaPresencialeExterior)
        {
            if (ModelState.IsValid)
            {
                db.ProgramaPresencialeExterior.Add(programaPresencialeExterior);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(programaPresencialeExterior);
        }

        // GET: ProgramaPresencialeExterior/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaPresencialeExterior programaPresencialeExterior = await db.ProgramaPresencialeExterior.FindAsync(id);
            if (programaPresencialeExterior == null)
            {
                return HttpNotFound();
            }
            return View(programaPresencialeExterior);
        }

        // POST: ProgramaPresencialeExterior/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,CODIGO_PROGRAMA,PROGRAMA,CINE_CAMPO_DETALLADO,PAIS,MODALIDAD,FECHA_PERIODO")] ProgramaPresencialeExterior programaPresencialeExterior)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programaPresencialeExterior).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(programaPresencialeExterior);
        }

        // GET: ProgramaPresencialeExterior/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramaPresencialeExterior programaPresencialeExterior = await db.ProgramaPresencialeExterior.FindAsync(id);
            if (programaPresencialeExterior == null)
            {
                return HttpNotFound();
            }
            return View(programaPresencialeExterior);
        }

        // POST: ProgramaPresencialeExterior/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProgramaPresencialeExterior programaPresencialeExterior = await db.ProgramaPresencialeExterior.FindAsync(id);
            db.ProgramaPresencialeExterior.Remove(programaPresencialeExterior);
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

                    List<ProgramaPresencialeExterior> listaProgramaPresencialeExterior = new List<ProgramaPresencialeExterior>();
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
                        return View("Index", db.ProgramaPresencialeExterior.ToList());
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
                        db.ProgramaPresencialeExterior.AddRange(listaProgramaPresencialeExterior);
                        db.SaveChanges();
                        return View("Index", db.ProgramaPresencialeExterior.ToList());
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

            List<ProgramaPresencialeExterior> listaProgramaPresencialeExterior = new List<ProgramaPresencialeExterior>();

            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaProgramaPresencialeExterior.Add(
                            new ProgramaPresencialeExterior()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                ANO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_PROGRAMA = matrixValorHoja[i, j++],
                                PROGRAMA = matrixValorHoja[i, j++],
                                CINE_CAMPO_DETALLADO = matrixValorHoja[i, j++],
                                PAIS = matrixValorHoja[i, j++],
                                MODALIDAD = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ProgramaPresencialeExterior.AddRange(listaProgramaPresencialeExterior);
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

            var lista = db.ProgramaPresencialeExterior.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "ProgramaPresencialeExt";

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
