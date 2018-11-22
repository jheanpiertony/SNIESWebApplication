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
                                Carga<ActividadBienestar>(hoja);
                                
                                //var nroColumna = hoja.Dimension.End.Column;
                                //var nroFila = hoja.Dimension.End.Row;

                                //for (int j = 2; j <= nroFila; j++)
                                //{
                                //    for (int y = 0; y <= nroColumna; y++)
                                //    {

                                //        //libro = new Libro()
                                //        //{
                                //        //    Titulo = workSheet.Cells[i, 1].Value.ToString(),
                                //        //    Autor = workSheet.Cells[i, 2].Value.ToString(),
                                //        //    ISBN = workSheet.Cells[i, 3].Value.ToString(),
                                //        //    Descripcion = workSheet.Cells[i, 4].Value.ToString(),
                                //        //    Ano = int.Parse(workSheet.Cells[i, 5].Value.ToString()),
                                //        //    Editorial = workSheet.Cells[i, 6].Value.ToString(),
                                //        //    Edicion = int.Parse(workSheet.Cells[i, 7].Value.ToString()),
                                //        //    Cantidad = int.Parse(workSheet.Cells[i, 8].Value.ToString()),
                                //        //    Precio = decimal.Parse(workSheet.Cells[i, 9].Value.ToString())
                                //        //};
                                //    }
                                //}
                            }                           
                        }
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
                                    listaActividadBienestar.Add(new ActividadBienestar()
                                    {
                                        ID_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                        NOMBRE_IES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                        ANO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                        SEMESTRE = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                        COD_UNIDAD = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                        UNIDAD_ORGANIZACIONAL = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                        COD_ACTIVIDAD = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                        ACTIVIDAD = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                        COD_TIPO_ACTIVIDAD = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                        TIPO_ACTIVIDAD = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                        FECHA_INICIO = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                        FECHA_FINAL = string.IsNullOrEmpty(row.Split(';')[11].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[11].Replace("\"", string.Empty),
                                        FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                    });
                                }
                                i++;
                            }
                        }
                    }


                    db.ActividadBienestar.AddRange(listaActividadBienestar);
                    db.SaveChanges();
                    return View("Index", db.ActividadBienestar.ToList());
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


        public void Carga<T>(ExcelWorksheet hoja) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            T a = new T();
            dynamic modeloT = new ExpandoObject();
            
            //a.

            var nroFila = hoja.Dimension.End.Row;
            var nroColumna = hoja.Dimension.End.Column;
            object[] values = new object[nroColumna];


            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            ob


            var lista = new List<T>();
            
            for (int i = 2; i <= nroFila; i++)
            {
                for (int j = 0; j <= nroColumna; j++)
                {
                    //var a = 
                }

            }
            //return new List<T>();
        }


        /// <summary>
        /// Create data table from list.
        /// https://stackoverflow.com/questions/18746064/using-reflection-to-create-a-datatable-from-a-class
        /// </summary>
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            int j = 0;

            foreach (PropertyInfo info in properties)
            {
                j++;
                //if (typeof(T).GetProperty(info.Name).GetGetMethod().IsVirtual )
                //{
                //    //if (info.PropertyType.IsGenericType)
                //    //{
                //    //    dataTable.Columns.AddRange(_createDateTable(info));
                //    //}
                //}
                //else
                //{
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                //}                
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    //if (typeof(IEnumerable).IsAssignableFrom(entity.GetType().GetTypeInfo()))
                    //{

                    //}
                    //else
                    //{
                    values[i] = properties[i].GetValue(entity);
                    //}                    
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
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
