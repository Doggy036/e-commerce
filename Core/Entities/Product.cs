using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        // [Key]//主鍵，會自動把Id看作是主鍵，若名稱不是Id，則需要加這行。
        public int Id { get; set; }
        public string Name { get; set; }
    }
}