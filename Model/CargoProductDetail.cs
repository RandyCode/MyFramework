using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("cargo_product_detail")]
    public class CargoProductDetail : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string product_id;
        [DataField("product_id")]
        public string ProductId
        {
            get { return product_id; }
            set { product_id = value; }
        }

        private string price;
        [DataField("price")]
        public string Price
        {
            get { return price; }
            set { price = value; }
        }


        private string description;
        [DataField("description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        private string product_name;
        [DataField("product_name")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }

        private string product_image;
        [DataField("product_image")]
        public string ProductImage
        {
            get { return product_image; }
            set { product_image = value; }
        }



        private DateTime createtime;
        [DataField("createtime")]
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }

    }
}
