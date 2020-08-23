using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly BoBeneficiario _bo;

        public BeneficiarioController(BoBeneficiario boBeneficiario)
        {
            _bo = boBeneficiario;
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel modelo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = ModelState.Values.SelectMany(item => item.Errors, (item, error) => error.ErrorMessage)
                        .ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                modelo.Id = _bo.Incluir(new Beneficiario
                {
                    CPF = modelo.CPF,
                    Nome = modelo.Nome,
                    IdCliente = modelo.IdCliente
                });
                return Json(true);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult Listar(BeneficiarioModel modelo)
        {
            return Json(_bo.Listar(modelo.IdCliente));
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel modelo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = (ModelState.Values.SelectMany(item => item.Errors, (item, error) => error.ErrorMessage))
                        .ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                _bo.Alterar(new Beneficiario
                {
                    CPF = modelo.CPF,
                    Nome = modelo.Nome,
                    Id = modelo.Id
                });
                return Json("Beneficiario alterado");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult Excluir(BeneficiarioModel modelo)
        {
            try
            {
                _bo.Excluir(modelo.Id);
                return Json("Beneficiario excluído");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}