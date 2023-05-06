using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace InventoryManagement.Controllers
{
    public class ProfileController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("InventoryManagementDB"));
        }

        public UserModel getUserDetails()
        {
            UserModel user = new();
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"Select * from users where uidd = {globalVar.id}";
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.uidd = (int)reader["uidd"];
                    user.name = (string)reader["uname"];
                    user.phoneNo = (string)reader["phoneNo"];
                    user.emailid = (string)reader["emailid"];
                    user.city = (string)reader["city"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
            }

            _Connection.Close();
            return user;
        }
        // GET: ProfileController
        public ActionResult Index()
        {
            if(globalVar.id != -1)
            {
                return View(getUserDetails());
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
            
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(login log)
        {
            try
            {
                _Connection.Open();
                SqlCommand cmd = _Connection.CreateCommand();
                Console.WriteLine(log.email + log.password);

                cmd.CommandText = $"Select * from users where emailid = '{log.email}'";
                Console.WriteLine(cmd);
                SqlDataReader reader = cmd.ExecuteReader();
                //Console.WriteLine(log.password == (string)reader["pass"]);
                while (reader.Read())
                {
                    if(log.password == (string)reader["pass"])
                    {
                        globalVar.id = (int)reader["uidd"];
                        globalVar.uname = (string)reader["uname"];
                        _Connection.Close();
                        return RedirectToAction(nameof(Index)); 
                    }
                }
                _Connection.Close();

                return View();
            }
            catch(Exception ex)
            {
                _Connection.Close();
                return View();
            }
        }


        public ActionResult Logout(int id)
        {
            globalVar.uname = "LogIn";
            globalVar.id = -1;
            return RedirectToAction(nameof(Index));
        }
        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel user)
        {
            try
            {
                _Connection.Open();
                SqlCommand cmd = _Connection.CreateCommand();
                cmd.CommandText = $"insert into users values ('{user.name}','{user.phoneNo}', '{user.city}', '{user.emailid}','{user.pass1}','User')";
                var r = cmd.ExecuteNonQuery();
                _Connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _Connection.Close();
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(getUserDetails());
        }

        public void editUser(int id, UserModel user)
        {
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"UPDATE users SET uname = '{user.name}', phoneNo = '{user.phoneNo}', emailid = '{user.emailid}', city = '{user.city}' WHERE uidd = {globalVar.id}";

            var r = cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserModel user)
        {
            try
            {
                editUser(id, user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                return View(ex.ToString());
            }
        }


        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, changePassModel passM)
        {
            try
            {
                _Connection.Open();
                SqlCommand cmd = _Connection.CreateCommand();
                cmd.CommandText = $"UPDATE users SET pass = '{passM.pass1}' WHERE uidd = {globalVar.id}";

                var r = cmd.ExecuteNonQuery();
                _Connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Password Not Changed" + ex.ToString());
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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


    public class changePassModel
    {
        [Required]
        public string pass1 { get; set; }
        [Required]
        [Compare("pass1", ErrorMessage ="Password Didn't match")]
        public string pass2 { get; set; }
    }
}
