using Microsoft.AspNetCore.Mvc;
using MVCExamenMichely.Models;
using MVCExamenMichely.Services;
using System.Numerics;

namespace MVCExamenMichely.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApiPersonajes service;
        public SeriesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<PersonajeSerie> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }
        public async Task<IActionResult> Buscar()
        {
            List<string> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string serie)
        {
            PersonajeSerie ser = await this.service.FindPersonajesSerie(serie);
            return View(ser);
        }
        public async Task<IActionResult> Details(int id)
        {
            PersonajeSerie personaje = await this.service.FindPersonajes(id);
            return View(personaje);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajes(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonajeSerie per)
        {
            await this.service.InsertPersonajes(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            PersonajeSerie doc =    await this.service.FindPersonajes(id);
            return View(doc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonajeSerie per)
        {
            await this.service.UpdatePersonajes(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);
            return RedirectToAction("Index");
        }
    }
}