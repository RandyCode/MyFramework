using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace Service
{
    public class GenericException : Exception
    {

        protected string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public GenericException()
        {
        }

        public GenericException(Exception ex)
            : base("", ex)
        {
            this._message = ex.Message;
        }

        public GenericException(string message)
        {
            this._message = message;
        }

        public GenericException(string message, Exception ex)
            : base(message, ex)
        {
            this._message = message;
        }
    }


}
