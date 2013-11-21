using ORM;
using ORM.Attributes;
using System;

namespace E_BusinessModel
{
    [DataTable("business_authority")]
    public class BusinessAuthority : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string authority;
        [DataField("authority")]
        public string Authority
        {
            get { return authority; }
            set { authority = value; }
        }


    }
}
