using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public interface IQueueManager
    {
         MessagePriority Priority{get;set;}

         void DeleteQueue(string path);

         bool SendMessage<T>(T target, bool tran = true);

         T ReceiveMessage<T>();

         void AysncReceiveMessage<T>(Action<object, ReceiveCompletedEventArgs> action);

         T ReceiveMessageByPeek<T>();

         List<T> GetAllMessage<T>();
    }
}
