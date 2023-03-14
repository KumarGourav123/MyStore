using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; 
using System.Data.SqlClient;


namespace Mystore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public Clientinfo clientinfo = new Clientinfo();
        public String errorMessage = "";
        public String successmessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=203.114.103.150;Initial Catalog=testemp;User ID=zipzaperp;Password=ripun@123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select *from clients where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientinfo.id = Request.Form["id"];
            clientinfo.name = Request.Form["name"];                   
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];

            if (clientinfo.id.Length == 0 || clientinfo.name.Length == 0 || 
                clientinfo.email.Length == 0 || clientinfo.phone.Length == 0 ||
                 clientinfo.address.Length == 0)
            {
                errorMessage = "All  the fields are Required";
                return;

            }
            try
            {
                String connectionString = "Data Source=203.114.103.150;Initial Catalog=testemp;User ID=zipzaperp;Password=ripun@123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                     "SET name=@name, email=@email, phone=@phone, address=@address " +
                     "WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientinfo.name);
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.Parameters.AddWithValue("@id", clientinfo.id);
                        command.ExecuteNonQuery();
                    }


                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }



            Response.Redirect("/Clients/Index");
        }
    }
}

