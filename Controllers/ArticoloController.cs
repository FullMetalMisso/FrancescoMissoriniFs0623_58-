using Microsoft.AspNetCore.Mvc;
using Zalando.Models;

namespace Zalando.Controllers
{
    public class ArticoloController : Controller
    {
        [HttpGet]
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
            articolo.ImgDetails[0].Insert(0, img1);
            articolo.ImgDetails[1].Insert(0, img2);
            Db.Add(articolo);
            return RedirectToAction("Details", new { id = articolo.Id });

            }
       

    }
}
