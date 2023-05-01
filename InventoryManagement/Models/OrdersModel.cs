using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace InventoryManagement.Models
{
    public class OrdersModel
    {
        public int oid { get; set; }
        public int uidd { get; set; }
        public int pid { get; set; }

        public int quantity { get; set; }

        public string ostatus { get; set; }
        public DateTime odate { get; set; }
    }
}
