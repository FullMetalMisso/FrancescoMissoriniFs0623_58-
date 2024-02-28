using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace Zalando.Models
{
    public static class Db
    {
        private static int _maxId = 2;

        private static List<Articolo> _articoli = [
            new Articolo() { Id = 0, Name = "Stivale", Description = "É un bello stivale", ImgCover="https://th.bing.com/th/id/R.05c726598f03b8e74deffbb120c1807e?rik=N28ifn4XJXoISw&pid=ImgRaw&r=0", Price = 50, Deleted= false, ImgDetails=["https://images-na.ssl-images-amazon.com/images/I/41liOW8SAjL._SY395_QL70_.jpg", "https://th.bing.com/th/id/R.a08be4992a99021c15692d544991ab14?rik=T5l1O1bNDCxgKg&pid=ImgRaw&r=0"]},
            new Articolo() { Id = 1, Name = "Sneakers", Description = "Comodo", ImgCover="https://th.bing.com/th/id/OIP.pZGFD1RcJ7JwXTJ8UNWqaAHaFF?rs=1&pid=ImgDetMain", Price = 50, Deleted= false, ImgDetails=["https://th.bing.com/th/id/R.610ad1df76d928f4c54b8d26f07704a0?rik=zFkIpGtHXM5Wgw&pid=ImgRaw&r=0", "https://th.bing.com/th/id/OIP.EpBY3rsEuLrw2y1yRG10lgHaI4?w=500&h=600&rs=1&pid=ImgDetMain"] },
             new Articolo() { Id = 2, Name = "Ciabatte", Description = "Scomodo", ImgCover="https://th.bing.com/th/id/R.05c726598f03b8e74deffbb120c1807e?rik=N28ifn4XJXoISw&pid=ImgRaw&r=0", Price = 50, Deleted= false, ImgDetails=["https://images-na.ssl-images-amazon.com/images/I/41liOW8SAjL._SY395_QL70_.jpg", "https://th.bing.com/th/id/R.a08be4992a99021c15692d544991ab14?rik=T5l1O1bNDCxgKg&pid=ImgRaw&r=0"] }
        ];


        public static List<Articolo> GetAll()
        {
            List<Articolo> articoli = [];
            foreach(var art in _articoli)
            {
                if(art.Deleted == false) articoli.Add(art);
            }
            return articoli;
        }

        public static List<Articolo> GetAllDeleted()
        {
            List<Articolo> artDeleted = [];
            foreach (var art in _articoli)
            {
                if (art.Deleted ) artDeleted.Add(art);
            }
            return artDeleted;
        }
        
        public static Articolo? Recover(int IdToRecover)
        {
            int? index = findArtIndex(IdToRecover);
          if(index != null)
            {
                var artRecovered = _articoli[(int)index];
                artRecovered.Deleted = true;
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
                    artFound = false;

                    break;
                }
            }

            if (artFound)  return i;
            return null;
        }

        public static Articolo? GetById(int? id)
        {
            if (id == null) return null;
            for(int i = 0; i  < _articoli.Count; i++)
            
            {
                Articolo art= _articoli[i];
                if (art.Id == id) return art;
            }
            return null;
        }

        public static Articolo Add(string name, string descrizione, Articolo articolo)
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
            if (index != 0)
            {
                _articoli[(int)index].Name = articolo.Name;
                _articoli[(int)index].ImgCover = articolo.ImgCover;
                _articoli[(int)index].Deleted = articolo.Deleted;
                _articoli[(int)index].Price = articolo.Price;
                _articoli[(int)index].Description = articolo.Description;
                _articoli[(int)index].ImgDetails[0] = articolo.ImgDetails[0];
                _articoli[(int)index].ImgDetails[1] = articolo.ImgDetails[1];
                return _articoli[(int)index];
            }
            return null;
        }
        
        public static Articolo? SoftDeleted(int idToDelete)
        {
            int? deletedI = findArtIndex(idToDelete);
            if(deletedI != null) 
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

        internal static void Add(Articolo articolo)
        {
            throw new NotImplementedException();
        }
    }
           
     }
    
