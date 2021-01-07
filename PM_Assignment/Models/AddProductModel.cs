using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Assignment.Models
{
    public class AddProductModel
    {
        [Key]
        public System.Guid ItemId { get; set; }

        [DisplayName("Item Code")]
        public string ItemCode { get; set; }

        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        [DisplayName("Upload Small Image")]
        public string SmallImagePath { get; set; }

        [DisplayName("Upload Large Image")]
        public string LongImagePath { get; set; }

        public HttpPostedFileBase SmallImageFile { get; set; }
        public HttpPostedFileBase LongImageFile { get; set; }

        [DisplayName("Item Price")]
        public decimal ItemPrice { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}