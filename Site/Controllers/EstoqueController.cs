﻿using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Middleware.Converters.Interface;
using Site.Abstraction;
using X.PagedList;

namespace Site.Controllers
{
    public class EstoqueController : AbstractController
    {
        #region Construtor

        const int TamanhoPagina = 15;
        private readonly IProduto _produto;
        private readonly IToastrMensagem _toastrMensagem;

        public EstoqueController(IProduto produto, IToastrMensagem toastrMensagem)
        {
            _produto = produto;
            _toastrMensagem = toastrMensagem;
        }

        #endregion

        #region Consulta

        public async Task<IActionResult> Produtos(string s, int? p)
        {
            var listaDeRegistros = await _produto.ConsultaRegistros(null);
            var numPagina = p ?? 1;

            if (string.IsNullOrEmpty(s))
            {
                return View(listaDeRegistros.ToPagedList(numPagina, TamanhoPagina));
            }

            listaDeRegistros = listaDeRegistros.Where(x => x.Nome.ToLower().Contains(s.ToLower()));

            return View(listaDeRegistros.ToPagedList(numPagina, TamanhoPagina));
        }

        #endregion

        #region Ajax

        [HttpGet]
        public async Task<JsonResult> GetDetalhes(int id)
        {
            var registro = await _produto.GetAllAsync(x => x.Id == id);
            var result = registro.Select(x => new
            {
                descricao = x.Nome,
                estoque = x.Estoque
            }).FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        public async Task<bool> PostConfirmarEstoque(int id, int estoque)
        {
            if (estoque < 1)
                estoque = 0;

            return await _produto.AtualizaEstoque(id, estoque);
        }

        #endregion

    }
}