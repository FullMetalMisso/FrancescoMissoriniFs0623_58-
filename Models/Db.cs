using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace Zalando.Models
{
    public static class Db
    {
        private static int _maxId = 3;

        private static List<Articolo> _articoli = [
            new Articolo(){Id = 0, Name = "Stivale", Description = "Stivali da pescatore", ImgCover = "https://m.media-amazon.com/images/I/51C-Ch6bfgL._AC_UY900_.jpg", Price = 20, Deleted = false, ImgDetails =["https://m.media-amazon.com/images/I/51C-Ch6bfgL._AC_UY900_.jpg" , "https://th.bing.com/th/id/R.05c726598f03b8e74deffbb120c1807e?rik=N28ifn4XJXoISw&pid=ImgRaw&r=0"] },
            new Articolo(){Id = 1, Name = "Scarpa", Description = "Scarpa di Peppe", ImgCover = "https://www.gestoutlet.com/media/catalog/product/cache/87b8a5aea06f2f3e4bf5610540acc8e8/D/0/D0422_3.jpg", Price = 30, Deleted = false, ImgDetails =["https://www.gestoutlet.com/media/catalog/product/cache/87b8a5aea06f2f3e4bf5610540acc8e8/D/0/D0422_3.jpg", "https://th.bing.com/th/id/R.05c726598f03b8e74deffbb120c1807e?rik=N28ifn4XJXoISw&pid=ImgRaw&r=0"] },
            new Articolo(){Id = 2, Name = "Ciabatta", Description = "Ciabatta materna da lancio", ImgCover = "https://www.tentazionecalzature.it/3134139-medium_default/ciabatta-da-camera-milly-7200-royal.jpg", Price = 10, Deleted = false, ImgDetails =["https://m.media-amazon.com/images/I/51C-Ch6bfgL._AC_UY900_.jpg" , "https://www.tentazionecalzature.it/3134139-medium_default/ciabatta-da-camera-milly-7200-royal.jpg"] }
            ];
        public static List<Articolo> GetAll()
        {
            List<Articolo> articoli = [];
            foreach (var art in _articoli)
            {
                if (art.Deleted == false) articoli.Add(art);
            }
            return articoli;
        }
        public static List<Articolo> GetAllDeleted()
        {
            List<Articolo> artDeleted = [];
            foreach (var art in _articoli)
            {
                if (art.Deleted) artDeleted.Add(art);
            }
            return artDeleted;
        }

        public static Articolo? Recover(int IdToRecover)
        {
            int? index = findArtIndex(IdToRecover);
            if ((index != null))
            {
                var artRecovered = _articoli[(int)index];
                artRecovered.Deleted = false;
                return artRecovered;
            }
            return null;
        }

        private static int? findArtIndex(int id)
        {
            int i;
            bool artFound = false;
            for (i = 0; i < _articoli.Count; i++)
            {
                if (_articoli[i].Id == id)
                {
                    artFound = true;
                    break;
                }
            }

            if (artFound) return i;
            return null;
        }

        public static Articolo? GetById(int? id)
        {
            if (id == null) return null;
            for (int i = 0; i < _articoli.Count; i++)
            {
                Articolo art = _articoli[i];
                if (art.Id == id) return art;
            }
            return null;
        }
        public static Articolo Add(Articolo articolo)
        {
            _maxId++;
            articolo.Id = _maxId;
            articolo.Deleted = false;
            _articoli.Add(articolo);
            return articolo;
        }

        public static Articolo? Edit(Articolo articolo)
        {
            int? index = findArtIndex(articolo.Id);
            if (index != null)
            {
                _articoli[(int)index].Name = articolo.Name;
                _articoli[(int)index].ImgCover = articolo.ImgCover;
                _articoli[(int)index].Price = articolo.Price;
                _articoli[(int)index].Description = articolo.Description;
                _articoli[(int)index].ImgDetails[0] = articolo.ImgDetails[0];
                _articoli[(int)index].ImgDetails[1] = articolo.ImgDetails[1];
                return _articoli[(int)index];
            }
            return null;
        }

        public static Articolo? SoftDelete(int idToDelete)
        {
            int? deletedI = findArtIndex(idToDelete);
            if (deletedI != null)
            {
                var artDeleted = _articoli[(int)deletedI];
                artDeleted.Deleted = true;
                return artDeleted;
            }
            return null;
        }
        public static Articolo? HardDelete(int idToDelete)
        {
            int? deletedI = findArtIndex(idToDelete);
            if (deletedI != null)
            {
                var artDeleted = _articoli[(int)deletedI];
                _articoli.RemoveAt((int)deletedI);
                return artDeleted;
            }
            return null;
        }
    }
}