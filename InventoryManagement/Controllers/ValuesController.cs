using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/callme")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        IConfiguration _configuration;
        SqlConnection _Connection;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("InventoryManagementDB"));
        }

        public List<ProductModel> list = new List<ProductModel>();

        [HttpGet]
        public ActionResult GetDataProducts()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from product", _Connection);
                _Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    ProductModel prod = new();

                    prod.pid = (int)reader["pid"];
                    prod.pname = (string)reader["pname"];
                    prod.pcat = (string)reader["pcat"];
                    prod.brand = (string)reader["brand"];
                    prod.desc = (string)reader["pdesc"];
                    prod.sold = (int)reader["soldsofar"];
                    prod.avl = (int)reader["avl"];

                    list.Add(prod);
                }
                _Connection.Close();
                return Ok(list);
            }
            catch(Exception ex)
            {
                _Connection.Close();
                Console.WriteLine(ex.ToString());
                return NotFound(ex.ToString());
            }

        }
    }
}
