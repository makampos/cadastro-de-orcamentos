using Microsoft.Extensions.Configuration;
using orcamento_crud.Interface;
using orcamento_crud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace orcamento_crud.Entities
{
    public class ProductRepository : IProduct
    {

        string connectionString = @"Server=localhost;Database=HOME1;User Id=sa;Password=<YourStrong@Passw0rd>;";
      
        public void AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Insert into Product (Name, Price) Values(@Name, @Price)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProduct(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Delete from Product where ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlQuery = "Update Product set Name = @Name, Price = @Price where ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }


        public Product GetProduct(int? id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Product where ProductId = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                }
            }
            return product;          
        }

       

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> lstProducts = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductId, Name, Price from Product", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    lstProducts.Add(product);
                }
                con.Close();
            }
            return lstProducts;
        }
    }
}
