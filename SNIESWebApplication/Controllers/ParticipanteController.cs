namespace SNIESWebApplication.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using SNIESWebApplication.Models;
    using System.IO;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using OfficeOpenXml;
    using System.Data;
    using System.Reflection;
    using ClosedXML.Excel;
    using SNIESWebApplication.Helpers;
    using System.Data.SqlClient;
    using SNIESWebApplication.Models.StoredProcedure;

    public class ParticipanteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private SaintDb dbSaint = new SaintDb();


        // GET: Participante
        public async Task<ActionResult> Index()
        {
            return View(await db.Participantes.OrderBy(x => x.NUM_DOCUMENTO).ToListAsync());
        }

        // GET: Participante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participante participante = await db.Participantes.FindAsync(id);
            if (participante == null)
            {
                return HttpNotFound();
            }
            return View(participante);
        }

        // GET: Participante/Create
        public ActionResult Create()
        {
            //List<SNIES_Participantes_Posg_Result> saintParticipatePosg = new List<SNIES_Participantes_Posg_Result>();
            //List<SNIES_Participantes_Preg_Result> saintParticipatePreg = new List<SNIES_Participantes_Preg_Result>();
            ////List<SNIES_Inscritos_Preg_Result> saintInscritoPreg = new List<SNIES_Inscritos_Preg_Result>();
            ////List<SNIES_Inscritos_Posg_Result> saintInscritoPost = new List<SNIES_Inscritos_Posg_Result>();
            ////List<SNIES_Admitidos_Preg_Result> saintAdmitidoPreg = new List<SNIES_Admitidos_Preg_Result>();
            ////List<SNIES_Admitidos_Posg_Result> saintAdmitidoPost = new List<SNIES_Admitidos_Posg_Result>();

            //var listaPeriodo = db.Periodos.Select(x => new { x.FechaPeriodo }).ToList();
            //using (SAINTdbSp dbSaint = new SAINTdbSp())
            //{
            //    foreach (var item in listaPeriodo)
            //    {
            //        saintParticipatePosg.AddRange(dbSaint.SNIES_Participantes_Posg(item.FechaPeriodo.ToString()).ToList());
            //        saintParticipatePreg.AddRange(dbSaint.SNIES_Participantes_Preg(item.FechaPeriodo.ToString()).ToList());
            //        //saintInscritoPreg.AddRange(dbSaint.SNIES_Inscritos_Preg(item.FechaPeriodo.ToString()).ToList());
            //        //saintInscritoPost.AddRange(dbSaint.SNIES_Inscritos_Posg(item.FechaPeriodo.ToString()).ToList());
            //        //saintAdmitidoPreg.AddRange(dbSaint.SNIES_Admitidos_Preg(item.FechaPeriodo.ToString()).ToList());
            //        //saintAdmitidoPost.AddRange(dbSaint.SNIES_Admitidos_Posg(item.FechaPeriodo.ToString()).ToList());
            //    }

            //}

            ViewBag.PeriodoId = new SelectList(db.Periodos, "Id", "FechaPeriodo");
            return View();
        }

        // POST: Participante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CODIGO_IES,IDENTIFICADOR_SNIES,TIPO_DOCUMENTO,NUM_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,ID_SEXO,FECHA_NACIMIENTO,PAIS,MUNICIPIO,EMAIL_INSTITUCIONAL,DIRECCION_INSTITUCIONAL,EMAIL_PEROSNAL,CELULAR_PERSONAL,FECHA_EXPEDICION,ID_ESTADO_CIVIL")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                db.Participantes.Add(participante);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(participante);
        }

        // GET: Participante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participante participante = await db.Participantes.FindAsync(id);
            if (participante == null)
            {
                return HttpNotFound();
            }
            return View(participante);
        }

        // POST: Participante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CODIGO_IES,IDENTIFICADOR_SNIES,TIPO_DOCUMENTO,NUM_DOCUMENTO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO,ID_SEXO,FECHA_NACIMIENTO,PAIS,MUNICIPIO,EMAIL_INSTITUCIONAL,DIRECCION_INSTITUCIONAL,EMAIL_PEROSNAL,CELULAR_PERSONAL,FECHA_EXPEDICION,ID_ESTADO_CIVIL")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participante).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(participante);
        }

        // GET: Participante/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participante participante = await db.Participantes.FindAsync(id);
            if (participante == null)
            {
                return HttpNotFound();
            }
            return View(participante);
        }

        // POST: Participante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Participante participante = await db.Participantes.FindAsync(id);
            db.Participantes.Remove(participante);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: CargaPlantillExcelaMasivaCatalogo
        [HttpPost]
        public ActionResult CargaPlantillaExcel(HttpPostedFileBase plantillaCargaExcel, int PeriodoId)
        {


            

            if (plantillaCargaExcel != null && !string.IsNullOrEmpty(plantillaCargaExcel.FileName) && plantillaCargaExcel.ContentLength != 0)
            {
                if (plantillaCargaExcel.FileName.EndsWith("xls") || plantillaCargaExcel.FileName.EndsWith("xlsx") || plantillaCargaExcel.FileName.EndsWith("xlsm") || plantillaCargaExcel.FileName.EndsWith("csv"))
                {

                    List<SNIES_Participantes_Posg_Result> saintParticipatePosg = new List<SNIES_Participantes_Posg_Result>();
                    List<SNIES_Participantes_Preg_Result> saintParticipatePreg = new List<SNIES_Participantes_Preg_Result>();
                    //List<SNIES_Inscritos_Preg_Result> saintInscritoPreg = new List<SNIES_Inscritos_Preg_Result>();
                    //List<SNIES_Inscritos_Posg_Result> saintInscritoPost = new List<SNIES_Inscritos_Posg_Result>();
                    //List<SNIES_Admitidos_Preg_Result> saintAdmitidoPreg = new List<SNIES_Admitidos_Preg_Result>();
                    //List<SNIES_Admitidos_Posg_Result> saintAdmitidoPost = new List<SNIES_Admitidos_Posg_Result>();

                    var listaPeriodo = db.Periodos.Select(x => new { x.FechaPeriodo }).ToList();
                    using (SAINTdbSp dbSaint = new SAINTdbSp())
                    {
                        foreach (var item in listaPeriodo)
                        {
                            saintParticipatePosg.AddRange(dbSaint.SNIES_Participantes_Posg(item.FechaPeriodo.ToString()).ToList());
                            saintParticipatePreg.AddRange(dbSaint.SNIES_Participantes_Preg(item.FechaPeriodo.ToString()).ToList());
                            //saintInscritoPreg.AddRange(dbSaint.SNIES_Inscritos_Preg(item.FechaPeriodo.ToString()).ToList());
                            //saintInscritoPost.AddRange(dbSaint.SNIES_Inscritos_Posg(item.FechaPeriodo.ToString()).ToList());
                            //saintAdmitidoPreg.AddRange(dbSaint.SNIES_Admitidos_Preg(item.FechaPeriodo.ToString()).ToList());
                            //saintAdmitidoPost.AddRange(dbSaint.SNIES_Admitidos_Posg(item.FechaPeriodo.ToString()).ToList());
                        }

                    }

                    List<Participante> listaParticipantes = new List<Participante>();
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
                                var participante = new Participante()
                                {
                                    CODIGO_IES = string.IsNullOrEmpty(row.Split(';')[0].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[0].Replace("\"", string.Empty),
                                    IDENTIFICADOR_SNIES = string.IsNullOrEmpty(row.Split(';')[1].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[1].Replace("\"", string.Empty),
                                    TIPO_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[2].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[2].Replace("\"", string.Empty),
                                    NUM_DOCUMENTO = string.IsNullOrEmpty(row.Split(';')[3].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[3].Replace("\"", string.Empty),
                                    PRIMER_NOMBRE = string.IsNullOrEmpty(row.Split(';')[4].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[4].Replace("\"", string.Empty),
                                    SEGUNDO_NOMBRE = string.IsNullOrEmpty(row.Split(';')[5].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[5].Replace("\"", string.Empty),
                                    PRIMER_APELLIDO = string.IsNullOrEmpty(row.Split(';')[6].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[6].Replace("\"", string.Empty),
                                    SEGUNDO_APELLIDO = string.IsNullOrEmpty(row.Split(';')[7].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[7].Replace("\"", string.Empty),
                                    ID_SEXO = string.IsNullOrEmpty(row.Split(';')[8].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[8].Replace("\"", string.Empty),
                                    FECHA_NACIMIENTO = string.IsNullOrEmpty(row.Split(';')[9].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[9].Replace("\"", string.Empty),
                                    PAIS = string.IsNullOrEmpty(row.Split(';')[10].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[10].Replace("\"", string.Empty),
                                    MUNICIPIO = string.IsNullOrEmpty(row.Split(';')[11].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[11].Replace("\"", string.Empty),
                                    EMAIL_INSTITUCIONAL = string.IsNullOrEmpty(row.Split(';')[12].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[12].Replace("\"", string.Empty),
                                    DIRECCION_INSTITUCIONAL = string.IsNullOrEmpty(row.Split(';')[13].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[13].Replace("\"", string.Empty),
                                    EMAIL_PEROSNAL = string.IsNullOrEmpty(row.Split(';')[14].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[14].Replace("\"", string.Empty),
                                    CELULAR_PERSONAL = string.IsNullOrEmpty(row.Split(';')[15].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[15].Replace("\"", string.Empty),
                                    VERIFICADO_POR_FUENTE_OFICIAL = string.IsNullOrEmpty(row.Split(';')[16].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[16].Replace("\"", string.Empty),
                                    FUENTE = string.IsNullOrEmpty(row.Split(';')[17].Replace("\"", string.Empty)) ? string.Empty : row.Split(';')[17].Replace("\"", string.Empty),
                                    //FECHA_PERIODO = _FECHA_PERIODO.FechaPeriodo
                                };

                                var _partposg = saintParticipatePosg.Where(x => x.NUM_DOCUMENTO == participante.NUM_DOCUMENTO).Select(x => new { x.FECHA_EXPEDICION, x.ID_ESTADO_CIVIL }).FirstOrDefault();
                                var _partpreg = saintParticipatePreg.Where(x => x.NUM_DOCUMENTO == participante.NUM_DOCUMENTO).Select(x => new { x.FECHA_EXPEDICION, x.ID_ESTADO_CIVIL }).FirstOrDefault();

                                participante.FECHA_EXPEDICION = (_partpreg != null) ? _partpreg.FECHA_EXPEDICION : (_partposg != null)? _partposg.FECHA_EXPEDICION : string.Empty;
                                participante.ID_ESTADO_CIVIL = (_partpreg != null) ? _partpreg.ID_ESTADO_CIVIL : (_partposg != null)? _partposg.ID_ESTADO_CIVIL : string.Empty;

                                listaParticipantes.Add(participante);
                            }
                            i++;
                        }
                    }

                    db.Participantes.AddRange(listaParticipantes);
                    db.SaveChanges();
                    return View("Index", db.Participantes.ToList());
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

        //// POST: CargaPlantillExcelaMasivaCatalogo http://localhost:50026/Participante
        //[HttpPost]
        public void CrearPlantillaExcel()
        {
            CrearExcel excel = new CrearExcel();

            var lista = db.Participantes.
                Select(x=> new
                {
                    x.Id,
                    x.CODIGO_IES,
                    x.IDENTIFICADOR_SNIES,
                    x.TIPO_DOCUMENTO,
                    x.NUM_DOCUMENTO,
                    x.PRIMER_NOMBRE,
                    x.SEGUNDO_NOMBRE,
                    x.PRIMER_APELLIDO,
                    x.SEGUNDO_APELLIDO,
                    x.ID_SEXO,
                    x.FECHA_NACIMIENTO,
                    x.PAIS,
                    x.MUNICIPIO,
                    x.EMAIL_INSTITUCIONAL,
                    x.DIRECCION_INSTITUCIONAL,
                    x.EMAIL_PEROSNAL,
                    x.CELULAR_PERSONAL,
                    x.ID_ESTADO_CIVIL,
                    x.FECHA_EXPEDICION
                }).ToList();
            CrearExcelT(lista);
        }

        public void CrearExcelT<T>(List<T> lista)
        {
            CrearExcel excel = new CrearExcel();
            DataTable dt = excel.ToDataTable<T>(lista);
            string nombre = "Participantes";

            using (XLWorkbook wb = new XLWorkbook())//https://github.com/ClosedXML/ClosedXML <----- la libreria
            {
                wb.Worksheets.Add(dt, "Participantes");
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
