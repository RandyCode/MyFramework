using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("business_role_authority")]
    public class BusinessRoleAuthority : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string business_role_id;
        [DataField("business_role_id")]
        public string BusinessRoleId
        {
            get { return business_role_id; }
            set { business_role_id = value; }
        }

        private string business_authority_id;
        [DataField("business_authority_id")]
        public string BusinessAuthorityId
        {
            get { return business_authority_id; }
            set { business_authority_id = value; }
        }



    }
}
