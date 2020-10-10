using orcamento_crud.Interface;
using orcamento_crud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace orcamento_crud.Repository
{
    public class ClientRepository : IClient
    {
        string connectionString = @"Server=localhost;Database=HOME1;User Id=sa;Password=<YourStrong@Passw0rd>;";
        public void AddClient(Client client)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Insert into Client (Name, PhoneNumber) Values(@Name,@PhoneNumber)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteClient(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Delete from Client where ClientId = @ClientId";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ClientId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateClient(Client client)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlQuery = "Update Client set Name = @Name, PhoneNumber = @PhoneNumber where ClientId = @ClientId";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ClientId", client.ClientId);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Client GetClient(int? id)
        {
            Client client = new Client();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select  * from Client where ClientId = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    client.ClientId = Convert.ToInt32(rdr["ClientId"]);
                    client.Name = rdr["Name"].ToString();
                    client.PhoneNumber = rdr["PhoneNumber"].ToString();
                }
            }
            return client;
        }

        public IEnumerable<Client> GetlAllClients()
        {
            List<Client> lstClient = new List<Client>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ClientId, Name, PhoneNumber from Client", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Client client = new Client();
                    client.ClientId = Convert.ToInt32(rdr["ClientId"]);
                    client.Name = rdr["Name"].ToString();
                    client.PhoneNumber = rdr["PhoneNumber"].ToString();
                    lstClient.Add(client);
                }
                con.Close();           
            }
            return lstClient;
        }
        
    }
}
