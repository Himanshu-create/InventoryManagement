using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    public class ProductUController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public ProductUController(IConfiguration configuration)
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
        // GET: ProductUController
        public ActionResult Index()
        {
            return View(getAllProduct());
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
        // GET: ProductUController/Details/5

        public ActionResult Details(int id)
        {
            return View(getProdId(id));
        }

        public  ActionResult PlaceOrder(int id)
        {
            globalVar.pid = id;
            if(globalVar.id == -1)
            {
                return RedirectToAction("Login", "Profile");
            }
            return RedirectToAction("Create", "OrderU");
        }
        // GET: ProductUController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductUController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductUController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductUController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductUController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductUController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
