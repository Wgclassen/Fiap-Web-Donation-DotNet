﻿using Fiap.Web.Donation5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Donation5.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ClienteModel clienteModel)
        {
            clienteModel.ClienteId = new Random().Next();

            return View("PostSucesso", clienteModel);
        }

    }
}
