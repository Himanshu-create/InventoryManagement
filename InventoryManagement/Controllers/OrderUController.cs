using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    public class OrderUController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public OrderUController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("InventoryManagementDB"));
        }
        // GET: OrderUController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderUController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderUController/Create
        void getProd()
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from product where pid = {globalVar.pid}";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {    
                globalVar.pname = (string)reader["pname"];

                globalVar.pavl = (int)reader["avl"];
            }


            _Connection.Close();
        }


        public ActionResult Create()
        {
            getProd();
            return View();
        }

        void createOrder(OrdersModel ord)
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"insert into orders values ({globalVar.id}, {globalVar.pid}, {ord.quantity}, 'Order Received', '{DateTime.Now}')";
            var n = cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        // POST: OrderUController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdersModel ord)
        {
            try
            {
                createOrder(ord);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderUController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderUController/Edit/5
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

        // GET: OrderUController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderUController/Delete/5
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
