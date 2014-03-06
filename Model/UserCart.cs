using ORM;
using ORM.Attributes;

namespace Model
{
    [DataTable("user_cart")]
    public class UserCart:DBObject
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
        public string User_id
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
        private int product_count;
             [DataField("product_count")]
        public int ProductCount
        {
            get { return product_count; }
            set { product_count = value; }
        }
        private double product_total;
             [DataField("product_total")]
        public double ProductTotal
        {
            get { return product_total; }
            set { product_total = value; }
        }
    }
}
