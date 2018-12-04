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
    public class MovilidadAdministrativoExteriorInternacionalizacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovilidadAdministrativoExteriorInternacionalizacion
        public async Task<ActionResult> Index()
        {
            return View(await db.MovilidadAdministrativoExteriorInternacionalizacion.ToListAsync());
        }

        // GET: MovilidadAdministrativoExteriorInternacionalizacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion = await db.MovilidadAdministrativoExteriorInternacionalizacion.FindAsync(id);
            if (movilidadAdministrativoExteriorInternacionalizacion == null)
            {
                return HttpNotFound();
            }
            return View(movilidadAdministrativoExteriorInternacionalizacion);
        }

        // GET: MovilidadAdministrativoExteriorInternacionalizacion/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: MovilidadAdministrativoExteriorInternacionalizacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PAIS,INSTITUCION_EXTRANJERA,TIPO_MOVILIDAD,DIAS_MOVILIDAD,CODIGO_CONVENIO,FECHA_PERIODO")] MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion)
        {
            if (ModelState.IsValid)
            {
                db.MovilidadAdministrativoExteriorInternacionalizacion.Add(movilidadAdministrativoExteriorInternacionalizacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(movilidadAdministrativoExteriorInternacionalizacion);
        }

        // GET: MovilidadAdministrativoExteriorInternacionalizacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion = await db.MovilidadAdministrativoExteriorInternacionalizacion.FindAsync(id);
            if (movilidadAdministrativoExteriorInternacionalizacion == null)
            {
                return HttpNotFound();
            }
            return View(movilidadAdministrativoExteriorInternacionalizacion);
        }

        // POST: MovilidadAdministrativoExteriorInternacionalizacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,ANO,SEMESTRE,ID_TIPO_DOCUMENTO,TIPO_DOCUMENTO,NUMERO_DOCUMENTO,PAIS,INSTITUCION_EXTRANJERA,TIPO_MOVILIDAD,DIAS_MOVILIDAD,CODIGO_CONVENIO,FECHA_PERIODO")] MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movilidadAdministrativoExteriorInternacionalizacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movilidadAdministrativoExteriorInternacionalizacion);
        }

        // GET: MovilidadAdministrativoExteriorInternacionalizacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion = await db.MovilidadAdministrativoExteriorInternacionalizacion.FindAsync(id);
            if (movilidadAdministrativoExteriorInternacionalizacion == null)
            {
                return HttpNotFound();
            }
            return View(movilidadAdministrativoExteriorInternacionalizacion);
        }

        // POST: MovilidadAdministrativoExteriorInternacionalizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovilidadAdministrativoExteriorInternacionalizacion movilidadAdministrativoExteriorInternacionalizacion = await db.MovilidadAdministrativoExteriorInternacionalizacion.FindAsync(id);
            db.MovilidadAdministrativoExteriorInternacionalizacion.Remove(movilidadAdministrativoExteriorInternacionalizacion);
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
                                //GuardarDatos(matrixValorHoja, hoja.Index, _FECHA_PERIODO.FechaPeriodo);
                            }
                        }
                        return View("Index", db.MovilidadAdministrativoExteriorInternacionalizacion.ToList());
                    }
                    else
                    {
                        string extesion = Path.GetExtension(fileName);
                        plantillaCargaExcel.SaveAs(filePath);
                        string csvData = System.IO.File.ReadAllText(filePath);
                        int i = 0;
                        List<MovilidadAdministrativoExteriorInternacionalizacion> listaMovilidadAdministrativoExteriorInternacionalizacion = new List<MovilidadAdministrativoExteriorInternacionalizacion>();

                        foreach (var row in csvData.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                if (i != 0)
                                {
                                    listaMovilidadAdministrativoExteriorInternacionalizacion.Add(new MovilidadAdministrativoExteriorInternacionalizacion()
                                    {
                                        CODIGO_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                        NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                        ANO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                        SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                        ID_TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                        TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                        NUMERO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                        PAIS = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                        INSTITUCION_EXTRANJERA = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                        TIPO_MOVILIDAD = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                        DIAS_MOVILIDAD = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                        CODIGO_CONVENIO = string.IsNullOrEmpty(row.Split(';')[11].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[11].Replace("\"", string.Empty),
                                        FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                    });
                                }
                                i++;
                            }
                        }
                        db.MovilidadAdministrativoExteriorInternacionalizacion.AddRange(listaMovilidadAdministrativoExteriorInternacionalizacion);
                        db.SaveChanges();
                        return View("Index", db.MovilidadAdministrativoExteriorInternacionalizacion.ToList());
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

            List<ActividadBienestar> listaActividadBienestar = new List<ActividadBienestar>();

            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaActividadBienestar.Add(
                            new ActividadBienestar()
                            {
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.ActividadBienestar.AddRange(listaActividadBienestar);
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

            var lista = db.MovilidadAdministrativoExteriorInternacionalizacion.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "MovilidadAdminExtInter";

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
