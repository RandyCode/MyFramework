
namespace ORM
{
    /// <summary>
    /// A1:all the model must inherit this class
    /// A2:create data must use GUID as uniquence primary key
    /// </summary>
   public class DBObject
    {
        private CURDActionEnum _dbActionType;

        public CURDActionEnum DbActionType
        {
            get { return _dbActionType; }
            set { _dbActionType = value; }
        }
    }
}
