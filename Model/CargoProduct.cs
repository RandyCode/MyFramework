using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("cargo_product")]
    public class CargoProduct : DBObject
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

        private DateTime createtime;
        [DataField("createtime")]
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }

        private string cargo_kinds_type_id;
        [DataField("cargo_kinds_type_id")]
        public string CargoKindsTypeId
        {
            get { return cargo_kinds_type_id; }
            set { cargo_kinds_type_id = value; }
        }

    }
}
