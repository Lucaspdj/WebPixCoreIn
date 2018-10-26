﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebPixCoreIn.Models;

namespace WebPixCoreIn.Controllers
{
    public class MotorAuxController : Controller
    {
        private WebPixCoreInContext db = new WebPixCoreInContext();

        // GET: MotorAux
        public ActionResult Index()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.ToList();

            return View(MotorAuxFiltrado);
        }

        // GET: MotorAux/Details/5
        public ActionResult Details(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.Where(x=> x.ID == id).FirstOrDefault();

            return View(MotorAuxFiltrado);
        }

        // GET: MotorAux/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModalAcoes(int? id)
        {
            if (id != null)
            {
                var acoes = GetAcoes(Convert.ToInt32(id));
                var motor = GetMotorAuxilixar(Convert.ToInt32(id));
                var tipoAcao = GetTipoAcao();

                ViewBag.Motor = motor.Nome;
                foreach (var acao in acoes)
                {
                    acao.MotorAuxiliar = motor.Nome;
                    acao.TipoAcao = tipoAcao.Nome;
                }

                return View(acoes);
            }

            return View();
        }

        //Implementar no InAPI
        private TipoAcaoViewModel GetTipoAcao()
        {
            return new TipoAcaoViewModel();
        }

        private MotorAuxViewModel GetMotorAuxilixar(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var motores = jss.Deserialize<IEnumerable<MotorAuxViewModel>>(result);

            var motor = motores.Where(m => m.ID.Equals(id)).SingleOrDefault();

            return motor;
        }

        // POST: MotorAux/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Metodo,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] MotorAuxViewModel motorAuxViewModel)
        {
            if (ModelState.IsValid)
            {
                motorAuxViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "MotorAux/Save";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = motorAuxViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(motorAuxViewModel);
        }

        // GET: MotorAux/Edit/5
        public ActionResult Edit(int? id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "MotorAux/GetAll";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            MotorAuxViewModel[] MotorAux = jss.Deserialize<MotorAuxViewModel[]>(result);

            var MotorAuxFiltrado = MotorAux.Where(x => x.ID == id).FirstOrDefault();

            return View(MotorAuxFiltrado);
        }

        // POST: MotorAux/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Metodo,Url,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] MotorAuxViewModel motorAuxViewModel)
        {
            if (ModelState.IsValid)
            {
                motorAuxViewModel.DataCriacao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.DateAlteracao = Convert.ToDateTime("01/08/1993");
                motorAuxViewModel.idCliente = 0;
                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
                    var url = keyUrl + "MotorAux/Save";
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = motorAuxViewModel;
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }


            return View(motorAuxViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<AcaoViewModel> GetAcoes(int motorId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlApiIn"].ToString();
            var url = keyUrl + "Acao";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var resultAcoes = jss.Deserialize<AcaoViewModel[]>(result);

            var acoes = resultAcoes.Where(r => r.idMotorAux.Equals(motorId));
            return acoes;
        }
    }
}
