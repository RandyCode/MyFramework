﻿using ORM;
using ORM.Attributes;
using System;

namespace Model
{
    [DataTable("business_user")]
    public class BusinessUser : DBObject
    {
        private string id;
        [DataField("id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime createtime;
        [DataField("create_datetime")]
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }

        private string business_role_id;
        [DataField("business_role_id")]
        public string BusinessRoleId
        {
            get { return business_role_id; }
            set { business_role_id = value; }
        }

        private string user_name;
        [DataField("user_name")]
        public string UserName
        {
            get { return user_name; }
            set { user_name = value; }
        }


        private string user_password;
        [DataField("user_password")]
        public string UserPassWord
        {
            get { return user_password; }
            set { user_password = value; }
        }

        private int user_idcard;
        [DataField("user_idcard")]
        public int UserIDCard
        {
            get { return user_idcard; }
            set { user_idcard = value; }
        }

    }
}