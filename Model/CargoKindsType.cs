using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("cargo_kinds_type")]
    public class CargoKindsType : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [DataField("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string type;
        [DataField("type")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private DateTime createtime;
        [DataField("createtime")]
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }


        private string cargo_kinds_id;
        [DataField("cargo_kinds_id")]
        public string CargoKindsId
        {
            get { return cargo_kinds_id; }
            set { cargo_kinds_id = value; }
        }
    }
}
