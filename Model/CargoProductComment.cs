using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("cargo_product_comment")]
    public class CargoProductComment : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string user_id;
        [DataField("user_id")]
        public string UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private string product_id;
        [DataField("product_id")]
        public string ProductId
        {
            get { return product_id; }
            set { product_id = value; }
        }

        private string message;
        [DataField("message")]
        public string Message
        {
            get { return message; }
            set { message = value; }
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
