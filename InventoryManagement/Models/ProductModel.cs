﻿namespace InventoryManagement.Models
{
    public class ProductModel
    {
        public int pid { get; set; }
        public string pname  {get; set;}
        public string pcat { get; set; }
        public string brand { get; set; }
        public string desc { get; set; }

        public int sold { get; set; }
        public int avl { get; set; }
    }
}
