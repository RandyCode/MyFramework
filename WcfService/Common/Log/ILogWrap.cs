using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public interface ILogWrap
    {
        void Write(string message);
        
        void Write(string message, LogMediaEnum[] mediaArray);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">msg</param>
        /// <param name="mediaArray">type</param>
        /// <param name="obj">new {Email="",ConnectionStr="" }</param>
        void Write(string message, LogMediaEnum[] mediaArray, object obj);
    }
}
