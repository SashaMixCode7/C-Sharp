namespace WebApplication_Store.Models
{
    public class Goods
    {
        public Goods(int id, string name, string description, string image_link, decimal price) 
        {
            this.Id = Id;
            this.Name = name;
            this.Description = description;
            this.Image_link = image_link;
            this.Price = price;
        }

        public Goods() { }


        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image_link { get; set; } = "";
        public decimal Price { get; set; }

    }
}
