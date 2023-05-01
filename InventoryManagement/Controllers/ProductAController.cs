using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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



            return allProd;
        }
        // GET: ProductAController
        public ActionResult Index()
        {
            return View(getAllProduct());
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

        // POST: ProductAController/Create
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

        // GET: ProductAController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductAController/Edit/5
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

        // GET: ProductAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductAController/Delete/5
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
