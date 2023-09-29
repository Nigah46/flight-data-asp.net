using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace newproject.Pages
{
    public class CreateModel : PageModel
        // configuarations for connecting with data source
    {
        private readonly string connectionString;
        public CreateModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        



        public CustomerInfo customerInfo = new();
        public string errormessage = "";
        public string successmessage = "";






        public void OnPost()
        {
            customerInfo.id = Request.Form["id"];
            customerInfo.name = Request.Form["name"];
            customerInfo.address = Request.Form["address"];
            customerInfo.contact = Request.Form["contact"];
            customerInfo.destination = Request.Form["destination"];
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();

                    string query = "INSERT INTO customers" +
                        "(id , name , contact ,address, destination) VALUES" +
                        "(@id , @name , @contact , @address , @destination);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", customerInfo.id);
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@contact", customerInfo.contact);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@destination", customerInfo.destination);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errormessage = "problem with your credentials ";
                Console.WriteLine(ex.ToString());
            }
            Response.Redirect("/CustomerList");
           // successmessage = "your data has been accepted ";

            customerInfo.id = "";
            customerInfo.name = "";
            customerInfo.contact = "";
            customerInfo.address = "";
            customerInfo.destination = "";

            
        }

    }
}
