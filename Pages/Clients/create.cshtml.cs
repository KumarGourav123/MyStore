using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Mystore.Pages.Clients
{
    public class createModel : PageModel
    {
        public Clientinfo clientinfo = new Clientinfo();
        public String errorMessage = "";
        public String successmessage = "";


        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];

            if (clientinfo.name.Length == 0 || clientinfo.email.Length == 0 ||
            clientinfo.phone.Length == 0 || clientinfo.address.Length == 0)
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
                    String sql = "INSERT into clients" +
                     "(name,email,phone,address) Values" +
                     "(@name,@email,@phone,@address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientinfo.name);
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientinfo.name = ""; clientinfo.email = ""; clientinfo.phone = ""; clientinfo.address = "";
            successmessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }

    }
}
