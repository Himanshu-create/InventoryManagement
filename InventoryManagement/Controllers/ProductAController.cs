using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace InventoryManagement.Controllers
{
    public class ProductAController : Controller
    {


        IConfiguration _configuration;
        SqlConnection _Connection;
        public ProductAController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("InventoryManagementDB"));
        }

        public List<ProductModel> getAllProduct()
        { 
            List<ProductModel> allProd = new();

            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from product";
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

                allProd.Add(prod);
            }


            _Connection.Close();
            return allProd;
        }

        
        // GET: ProductAController
        public ActionResult Index2()
        {
            if(globalVar.id == 1)
            {

                string baseUrl = "https://localhost:7143/api/callme/";
                

                return View(getAllProduct());
                
            }
            else
            {
                return RedirectToAction("Index", "ProductU");
            }
            
        }

        public async Task<ActionResult> Index()
        {
            if (globalVar.id == 1)
            {
                Console.WriteLine("Inside API CAll");
                string Baseurl = "https://localhost:7143/";
                List<ProductModel> prods = new List<ProductModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/callme");
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        prods = JsonConvert.DeserializeObject<List<ProductModel>>(EmpResponse);
                        return View(prods);
                    }
                    return View();
                }

            }
            else
            {
                return RedirectToAction("Index", "ProductU");
            }
        }

        // GET: ProductAController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


         
        // GET: ProductAController/Create
        public ActionResult Create()
        {
            return View();
        }


        void CreateProduct(ProductModel prod)
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"insert into product values ('{prod.pname}', '{prod.pcat}', '{prod.brand}', '{prod.desc}', 0, {prod.avl})";
            var n = cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        // POST: ProductAController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel prod)
        {
            try
            {
                CreateProduct(prod);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductAController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(getProdId(id));
        }

        void editProd(ProductModel prod, int id)
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"UPDATE product SET pname = '{prod.pname}', pcat = '{prod.pcat}', brand = '{prod.brand}', pdesc = '{prod.desc}', soldsofar = {prod.sold}, avl = {prod.avl} WHERE pid = {id}";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message.ToString());
            }
            _Connection.Close();

        }
        // POST: ProductAController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ProductModel prod)
        {
            try
            {
                editProd(prod, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ProductModel getProdId(int id)
        {
            ProductModel prod = new();
            Console.WriteLine(id);
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from product where pid = {id}";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                prod.pid = (int)reader["pid"];
                prod.pname = (string)reader["pname"];
                prod.pcat = (string)reader["pcat"];
                prod.brand = (string)reader["brand"];
                prod.desc = (string)reader["pdesc"];
                prod.sold = (int)reader["soldsofar"];
                prod.avl = (int)reader["avl"];

            }


            _Connection.Close();

            return prod;
        }
        // GET: ProductAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(getProdId(id));
        }

        void deleteProd(int id)
        {
            ProductModel prod = new();
            Console.WriteLine(id);
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Delete from product where pid = {id}";
            var n = cmd.ExecuteNonQuery();

            _Connection.Close();
        }

        // POST: ProductAController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                deleteProd(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
