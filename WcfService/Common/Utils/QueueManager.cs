using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading.Tasks;


namespace CommonHelper
{
    /// <summary>
    /// 事务MSMQ
    /// </summary>
    public class QueueManager
    {

        private bool _transaction = false;
        private MessageQueue Queue { get; set; }

        public long MaxSize
        {
            get { return Queue.MaximumQueueSize; }
            set { Queue.MaximumQueueSize = value; }
        }

        /// <summary>
        /// 事务队列设置优先级无效
        /// </summary>
        public MessagePriority Priority
        {
            get { return (MessagePriority)Queue.BasePriority; }
            set { Queue.BasePriority = (short)value; }
        }


        public QueueManager(string path, bool transaction = false)
        {
            _transaction = transaction;
            try
            {
                if (!MessageQueue.Exists(path))
                {
                    if (_transaction == false)
                        Queue = MessageQueue.Create(path); 
                    else
                        Queue = MessageQueue.Create(path, true);
                }
                else
                {
                    Queue = new MessageQueue(path);
                }
                Priority = MessagePriority.Normal;
            }
            catch
            {
                throw new Exception("Fail to create message queue.");
            }
        }

        public void DeleteQueue(string path)
        {
            try
            {
                if (MessageQueue.Exists(path))
                {
                    MessageQueue.Delete(path);
                }
            }
            catch
            {
                throw new Exception("Fail to delete message queue.");
            }
        }

        public bool SendMessage<T>(T target, bool tran = true)
        {
            try
            {
                System.Messaging.Message myMessage = new System.Messaging.Message();
                myMessage.Body = target;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

                if (_transaction == false)
                {
                    myMessage.Priority = this.Priority;
                    Queue.Send(myMessage);
                }
                else
                {
                    var messageQueueTransaction = new MessageQueueTransaction();
                    messageQueueTransaction.Begin();
                    Queue.Send(myMessage, messageQueueTransaction);
                    messageQueueTransaction.Commit();
                }


                return true;
            }
            catch (ArgumentException e)
            {
                return false;
            }
        }

        public T ReceiveMessage<T>()
        {
            Queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                Message myMessage;
                if (_transaction == true)
                {
                    var tran = new MessageQueueTransaction();
                    tran.Begin();
                    myMessage = Queue.Receive(tran);
                    tran.Commit();

                }
                else
                {
                    myMessage = Queue.Receive();
                }


                return (T)myMessage.Body;
            }
            catch
            {
                throw new Exception("Fail to receive message.");
            }

        }


        public void AysncReceiveMessage<T>(Action<object, ReceiveCompletedEventArgs> action)
        {
            //var messageQueue = (MessageQueue)sender;
            //var message = messageQueue.EndReceive(e.AsyncResult);
            //var messageObj = message.Body;
            Queue.ReceiveCompleted += new ReceiveCompletedEventHandler(action);
            Queue.BeginReceive();
        }


        public T ReceiveMessageByPeek<T>()
        {
            //连接到本地队列

            Queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            //从队列中接收消息
            System.Messaging.Message myMessage = Queue.Peek();
            return (T)myMessage.Body; //获取消息的内容

        }

        public List<T> GetAllMessage<T>()
        {

            Queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            Message[] msgArr = Queue.GetAllMessages();
            List<T> list = new List<T>();
            msgArr.ToList().ForEach((o) =>
            {
                list.Add((T)o.Body);
            });
            return list;

        }

    }
}
