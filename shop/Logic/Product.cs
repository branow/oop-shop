using Shop.Logic.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic
{

    public class ProductDTO
    {
        public int Id { get; set; }
        public long Price { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }

    }

    public class Product
    {
        public static readonly List<string> Categories = new CategoryTable().Get();
        public int Id { get; }
        private long _price;
        public long Price
        {
            get { return _price; }
            set
            {
                if (value < 0) throw new InvalidDataException("price have to be > 0");
                else _price = value;
            }
        }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int SellerId { get; }

        public Product(ProductDTO product)
        {
            Id = product.Id;
            Price = product.Price;
            Name = product.Name;
            Description = product.Description;
            SellerId = product.SellerId;
            CategoryId = product.CategoryId;
        }

        public string GetShortInfo() 
        {
            return $"{Id} -- {Name} -- {Price/100} -- {Categories[CategoryId-1]}";
        }

        public string GetAllInfo()
        {
            return $"{Id} -- {Name} -- {Price / 100} -- {Categories[CategoryId - 1]}\n{Description}" +
                $"\n seller - {new AccountTable().Get(SellerId).Name}";
        }
        public override string ToString()
        {
            return $"{Id} {Categories[CategoryId - 1]} {Name} {Price/100} ({Description}) {SellerId}";
        }

        public override bool Equals(object? obj)
        {
            var o = obj as Product;
            if (o == null)
                return false;
            return Id == o.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }

}
