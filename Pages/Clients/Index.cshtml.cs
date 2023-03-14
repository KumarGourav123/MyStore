using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mystore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<Clientinfo> listClients = new List<Clientinfo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=203.114.103.150;Initial Catalog=testemp;User ID=zipzaperp;Password=ripun@123";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string Sql = "Select *From clients";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clientinfo clientinfo = new Clientinfo();
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                                clientinfo.created_at = reader.GetDateTime(5).ToString();
                                listClients.Add(clientinfo);


                            }



                        }

                    }




                }




            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }





        }
    }
    public class Clientinfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;

    }




}

