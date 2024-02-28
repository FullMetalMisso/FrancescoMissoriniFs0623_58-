namespace Zalando.Models
{
    public class Articolo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImgCover { get; set; }

        public bool Deleted { get; set; }

        public List<string> ImgDetails { get; set;}


    }
}
