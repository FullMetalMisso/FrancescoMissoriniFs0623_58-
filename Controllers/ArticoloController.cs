using Microsoft.AspNetCore.Mvc;
using Zalando.Models;

namespace Zalando.Controllers
{
    public class ArticoloController : Controller
    {
        public IActionResult Index()
        {
            return View(Db.GetAll());
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            if (id.HasValue)
            {
                var art = Db.GetById(id);
                if (art is null)
                {
                    return View("Error");
                }
                return View(art);
            }
            else
            {
                return RedirectToAction("Index", "Articolo");
            }

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string name, string descrizione, decimal price, string imgCover, string img1, string img2)
        {
            Articolo articolo = new Articolo();
            articolo.Name = name;
            articolo.Description = descrizione;
            articolo.Price = price;
            articolo.ImgCover = imgCover;

            if (articolo.ImgDetails == null)
            {
                articolo.ImgDetails = new List<string>();
            }

            if (articolo.ImgDetails.Count < 2)
            {
                articolo.ImgDetails.Add(img1);
                articolo.ImgDetails.Add(img2);
            }
            else
            {
                articolo.ImgDetails[0] = img1;
                articolo.ImgDetails[1] = img2;
            }

            Db.Add(articolo);

            return RedirectToAction("Details", new { id = articolo.Id });
        }
        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id.HasValue)
            {
                var art = Db.GetById(id);
                if (art is null)
                {
                    return View("Error");
                }
                return View(art);
            }
            else
            {
                return RedirectToAction("Index", "Articolo");
            }

        }

        [HttpPost]

        public IActionResult Edit(Articolo articolo)
        {
            var updateArt = Db.Edit(articolo);
            if (updateArt is null) return View("Error");
            return RedirectToAction("Details", new { id = articolo.Id });
        }
    }

}