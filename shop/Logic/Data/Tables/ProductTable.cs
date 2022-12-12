using MySql.Data.MySqlClient;
using Shop.Logic;
using Shop.Logic.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Logic.Data.Tables
{
    public class ProductTable : Table
    {

        private static readonly string s_table = "products", s_id = "product_id", s_name = "product_name",
            s_price = "price", s_category = "category", s_description = "product_description", s_seller = "seller_id";
        public ProductTable() : base(s_table, s_id)
        {
        }

        public Product Create(string name, long price, int categoryId, int sellerId, string description = "")
        {
            int id = Insert(name, price, categoryId, description, sellerId);
            ProductDTO product = new ProductDTO();
            product.Id = id;
            product.Name = name;
            product.Price = price;
            product.CategoryId = categoryId;
            product.SellerId = sellerId;
            product.Description = description;
            return new Product(product);
        }

        public List<Product> Get()
        {
            List<Product> list = new List<Product>();
            MySqlDataReader reader = Select();
            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.Id = reader.GetInt32(s_id);
                product.Name = reader.GetString(s_name);
                product.Price = reader.GetInt64(s_price);
                product.CategoryId = reader.GetInt32(s_category);
                product.SellerId = reader.GetInt32(s_seller);
                product.Description = reader.GetString(s_description);
                list.Add(new Product(product));
            }
            reader.Close();
            return list;
        }

        public Product Get(int id)
        {
            MySqlDataReader reader = Select(new Condition(new DataField(s_id, id)));
            reader.Read();
            ProductDTO product = new ProductDTO();
            product.Id = reader.GetInt32(s_id);
            product.Name = reader.GetString(s_name);
            product.Price = reader.GetInt64(s_price);
            product.CategoryId = reader.GetInt32(s_category);
            product.SellerId = reader.GetInt32(s_seller);
            product.Description = reader.GetString(s_description);
            reader.Close();
            return new Product(product);
        }

        public void Update(Product product)
        {
            Condition con = new Condition(new DataField(s_id, product.Id));
            Update(new DataField(s_name, product.Name), con);
            Update(new DataField(s_price, product.Price), con);
            Update(new DataField(s_category, product.CategoryId), con);
            Update(new DataField(s_description, product.Description), con);
            Update(new DataField(s_seller, product.SellerId), con);

        }

        public void Remove(int id)
        {
            Delete(new Condition(new DataField(s_id, id)));
        }
    }
}
