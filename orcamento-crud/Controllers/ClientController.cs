using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using orcamento_crud.Interface;
using orcamento_crud.Models;

namespace orcamento_crud.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClient cli;

        public ClientController(IClient client)
        {
            cli = client;
        }

        public IActionResult Index()
        {
            List<Client> lstClients = new List<Client>();
            lstClients = cli.GetlAllClients().ToList();
            return View(lstClients);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = cli.GetClient(id);

            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Client client)
        {
            if (ModelState.IsValid)
            {
                cli.AddClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = cli.GetClient(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cli.UpdateClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = cli.GetClient(id);

            if(client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            cli.DeleteClient(id);
            return RedirectToAction("Index");
        }

    }
}
