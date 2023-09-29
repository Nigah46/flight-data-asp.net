using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace newproject.Pages
{
    public class CustomerListModel : PageModel
    {
        private readonly string connectionString;
        public CustomerListModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

       // public List<InternInfo> listInterns = new List<InternInfo>();



        public List<CustomerInfo> customerList = new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source = DESKTOP - LOA8NMN; Initial Catalog = Test; Integrated Security = True";
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    string sql = "SELECT * FROM customers ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customers = new CustomerInfo();
                                customers.id = "" + reader.GetInt32(0);
                                customers.name = reader.GetString(1);
                                customers.address = reader.GetString(2);
                                customers.contact = reader.GetString(3);
                                customers.destination = reader.GetString(4);

                                customerList.Add(customers);


                            }
                        }
                    }
                }
            }

            catch (Exception ex)

            {
                Console.WriteLine("Exception " + ex.ToString());
            }

    }
}


    
}
