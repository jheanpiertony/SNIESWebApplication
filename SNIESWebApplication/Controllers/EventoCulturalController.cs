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

    [Authorize(Users = "calidad@unicoc.edu.co,desarrollador@unicoc.edu.co")]
    public class EventoCulturalController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventoCultural
        public async Task<ActionResult> Index()
        {
            return View(await db.EventoCultural.ToListAsync());
        }

        // GET: EventoCultural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoCultural eventoCultural = await db.EventoCultural.FindAsync(id);
            if (eventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(eventoCultural);
        }

        // GET: EventoCultural/Create
        public ActionResult Create()
        {
            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: EventoCultural/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] EventoCultural eventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.EventoCultural.Add(eventoCultural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eventoCultural);
        }

        // GET: EventoCultural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoCultural eventoCultural = await db.EventoCultural.FindAsync(id);
            if (eventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(eventoCultural);
        }

        // POST: EventoCultural/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,NOMBRE_IES,AÑO,SEMESTRE,CODIGO_UNIDAD,CODIGO_EVENTO,EVENTO,FECHA_INICIO,FECHA_FINAL,FECHA_PERIODO")] EventoCultural eventoCultural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventoCultural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventoCultural);
        }

        // GET: EventoCultural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoCultural eventoCultural = await db.EventoCultural.FindAsync(id);
            if (eventoCultural == null)
            {
                return HttpNotFound();
            }
            return View(eventoCultural);
        }

        // POST: EventoCultural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EventoCultural eventoCultural = await db.EventoCultural.FindAsync(id);
            db.EventoCultural.Remove(eventoCultural);
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

                    List<EventoCultural> listaEventoCultural = new List<EventoCultural>();
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
                        return View("Index", db.EventoCultural.ToList());
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
                        db.EventoCultural.AddRange(listaEventoCultural);
                        db.SaveChanges();
                        return View("Index", db.EventoCultural.ToList());
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

            List<EventoCultural> listaEventoCultural = new List<EventoCultural>();
            List<FteNacionEventoCultural> listaFteNacionEventoCultural = new List<FteNacionEventoCultural>();
            List<FteInternEventoCultural> listaFteInternEventoCultural = new List<FteInternEventoCultural>();
            List<RecHumanoEventoCultural> listaRecHumanoEventoCultural = new List<RecHumanoEventoCultural>();
            List<BeneficiarEventoCultural> listaBeneficiarEventoCultural = new List<BeneficiarEventoCultural>();


            for (int i = 0; i < nroFila; i++)
            {
                var j = 0;
                switch (index)
                {
                    case 1:

                        listaEventoCultural.Add(
                            new EventoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                AÑO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_UNIDAD = matrixValorHoja[i, j++],
                                CODIGO_EVENTO = matrixValorHoja[i, j++],
                                EVENTO = matrixValorHoja[i, j++],
                                FECHA_INICIO = matrixValorHoja[i, j++],
                                FECHA_FINAL = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.EventoCultural.AddRange(listaEventoCultural);
                            db.SaveChanges();
                        }
                        break;

                    case 2:
                        listaFteNacionEventoCultural.Add(
                            new FteNacionEventoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                AÑO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_UNIDAD = matrixValorHoja[i, j++],
                                CODIGO_EVENTO = matrixValorHoja[i, j++],
                                ID_FUENTE_NACIONAL = matrixValorHoja[i, j++],
                                FUENTE_NACIONAL = matrixValorHoja[i, j++],
                                VALOR_FINANCIACION = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FteNacionEventoCultural.AddRange(listaFteNacionEventoCultural);
                            db.SaveChanges();
                        }
                        break;

                    case 3:
                        listaFteInternEventoCultural.Add(
                            new FteInternEventoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                AÑO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_UNIDAD = matrixValorHoja[i, j++],
                                CODIGO_EVENTO = matrixValorHoja[i, j++],
                                EVENTO = matrixValorHoja[i, j++],
                                NOMBRE_INSTITUCION = matrixValorHoja[i, j++],
                                ID_PAIS = matrixValorHoja[i, j++],
                                PAIS = matrixValorHoja[i, j++],
                                ID_FUENTE_INTERNACIONAL = matrixValorHoja[i, j++],
                                FUENTE_INTERNACIONAL = matrixValorHoja[i, j++],
                                VALOR_FINANCIACION = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.FteInternEventoCultural.AddRange(listaFteInternEventoCultural);
                            db.SaveChanges();
                        }
                        break;

                    case 4:
                        listaRecHumanoEventoCultural.Add(
                            new RecHumanoEventoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                AÑO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_UNIDAD = matrixValorHoja[i, j++],
                                CODIGO_EVENTO = matrixValorHoja[i, j++],
                                EVENTO = matrixValorHoja[i, j++],
                                ID_TIPO_DOCUMENTO = matrixValorHoja[i, j++],
                                TIPO_DOCUMENTO = matrixValorHoja[i, j++],
                                NUMERO_DOCUMENTO = matrixValorHoja[i, j++],
                                PRIMER_NOMBRE = matrixValorHoja[i, j++],
                                SEGUNDO_NOMBRE = matrixValorHoja[i, j++],
                                PRIMER_APELLIDO = matrixValorHoja[i, j++],
                                SEGUNDO_APELLIDO = matrixValorHoja[i, j++],
                                DEDICACION = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.RecHumanoEventoCultural.AddRange(listaRecHumanoEventoCultural);
                            db.SaveChanges();
                        }
                        break;

                    case 5:
                        listaBeneficiarEventoCultural.Add(
                            new BeneficiarEventoCultural()
                            {
                                CODIGO_IES = matrixValorHoja[i, j++],
                                NOMBRE_IES = matrixValorHoja[i, j++],
                                AÑO = matrixValorHoja[i, j++],
                                SEMESTRE = matrixValorHoja[i, j++],
                                CODIGO_UNIDAD = matrixValorHoja[i, j++],
                                CODIGO_EVENTO = matrixValorHoja[i, j++],
                                ID_TIPO_BENEFICIARIO = matrixValorHoja[i, j++],
                                TIPO_BENEFICIARIO = matrixValorHoja[i, j++],
                                TOTAL_ASISTENTES = matrixValorHoja[i, j++],
                                VALOR_ENTRADA = matrixValorHoja[i, j++],
                                FECHA_PERIODO = _FECHA_PERIODO
                            }
                            );

                        if (i + 1 == nroFila)
                        {
                            db.BeneficiarEventoCultural.AddRange(listaBeneficiarEventoCultural);
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

            var lista = db.EventoCultural.ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {

            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = dt.TableName.ToString();

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
