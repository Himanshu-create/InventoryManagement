using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    public class OrderAController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public OrderAController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("InventoryManagementDB"));
        }

        public List<OrdersModel> getOrders()
        {
            List<OrdersModel> ords = new();
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from orders";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrdersModel ord = new();

                ord.oid = (int)reader["oid"];
                ord.pid = (int)reader["pid"];
                ord.quantity = (int)reader["quantity"];
                ord.ostatus = (string)reader["ostatus"];
                ord.odate = (DateTime)reader["odate"];

                ords.Add(ord);
            }

            return ords;
        }
        // GET: OrderAController
        public ActionResult Index()
        {
            return View(getOrders());
        }

        // GET: OrderAController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderAController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderAController/Create
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

        public ActionResult ViewProduct(int id)
        {
            return RedirectToAction($"deleteProd/{id}", "ProductA");
        }
        public OrdersModel getOrderID(int id)
        {
            OrdersModel ord = new();
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from orders where oid = {id}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                ord.oid = (int)reader["oid"];
                ord.pid = (int)reader["pid"];
                ord.quantity = (int)reader["quantity"];
                ord.ostatus = (string)reader["ostatus"];
                ord.odate = (DateTime)reader["odate"];

            }
            _Connection.Close();

            return ord;
        }
        // GET: OrderAController/Edit/5
        public ActionResult UpdateOrder(int id)
        {
            return View(getOrderID(id));
        }


        public void updateorderstatus(int oid, OrdersModel ord)
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"UPDATE orders SET ostatus = '{ord.ostatus}' WHERE oid = {oid}";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message.ToString());
            }
            _Connection.Close();
        }
        // POST: OrderAController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOrder(int id, OrdersModel ord)
        {
            try
            {
                updateorderstatus(id, ord);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderAController/Delete/5
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
