using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("business_role")]
    public class BusinessRole : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string role_name;
        [DataField("role_name")]
        public string RoleName
        {
            get { return role_name; }
            set { role_name = value; }
        }

        private DateTime create_datetime;
        [DataField("create_datetime")]
        public DateTime CreateDatetime
        {
            get { return create_datetime; }
            set { create_datetime = value; }
        }



    }
}
