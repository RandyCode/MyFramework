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
    public class QueueManager :IQueueManager
    {

        private bool _transaction = false;
        private MessageQueue _queue { get; set; }

        public long MaxSize
        {
            get { return _queue.MaximumQueueSize; }
            set { _queue.MaximumQueueSize = value; }
        }

        /// <summary>
        /// 事务队列设置优先级无效
        /// </summary>
        public MessagePriority Priority
        {
            get { return (MessagePriority)_queue.BasePriority; }
            set { _queue.BasePriority = (short)value; }
        }


        public QueueManager(string path, bool transaction = false)
        {
            _transaction = transaction;
            try
            {
                if (!MessageQueue.Exists(path))
                {
                    if (_transaction == false)
                        _queue = MessageQueue.Create(path); 
                    else
                        _queue = MessageQueue.Create(path, true);
                }
                else
                {
                    _queue = new MessageQueue(path);
                }
                Priority = MessagePriority.Normal;
            }
            catch
            {
                throw new Exception("Fail to initialize message queue.");
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
                    _queue.Send(myMessage);
                }
                else
                {
                    var messageQueueTransaction = new MessageQueueTransaction();
                    messageQueueTransaction.Begin();
                    _queue.Send(myMessage, messageQueueTransaction);
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
            _queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                Message myMessage;
                if (_transaction == true)
                {
                    var tran = new MessageQueueTransaction();
                    tran.Begin();
                    myMessage = _queue.Receive(tran);
                    tran.Commit();

                }
                else
                {
                    myMessage = _queue.Receive();
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
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(action);
            _queue.BeginReceive();
        }


        public T ReceiveMessageByPeek<T>()
        {
            //连接到本地队列

            _queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            //从队列中接收消息
            System.Messaging.Message myMessage = _queue.Peek();
            return (T)myMessage.Body; //获取消息的内容

        }

        public List<T> GetAllMessage<T>()
        {

            _queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            Message[] msgArr = _queue.GetAllMessages();
            List<T> list = new List<T>();
            msgArr.ToList().ForEach((o) =>
            {
                list.Add((T)o.Body);
            });
            return list;

        }

    }
}
