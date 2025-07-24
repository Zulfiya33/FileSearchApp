
namespace FileSearch.Core.Models
{
    public class Product
    {
        public string Name { get; }
        public float Price { get; }

        public Product(string name, float price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }
    }
}
