using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product:BaseEntity
    {
        // [Key]//主鍵，會自動把Id看作是主鍵，若名稱不是Id，則需要加這行。
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public ProductType ProductType { get; set; }//relationship

        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; }//relationship

        public int ProductBrandId { get; set; }
    }

}