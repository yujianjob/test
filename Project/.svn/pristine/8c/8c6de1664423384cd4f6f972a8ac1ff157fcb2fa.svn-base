using System;
using System.Runtime.Serialization;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    [Serializable]
    [DataContract]
    public class ReturnEntity<T>
    {
        private int _Code = 0;
        private string _Message = string.Empty;
        private string _Title = string.Empty;
        private T _Obj = default(T);

        public ReturnEntity()
        { }

        public ReturnEntity(T obj)
        {
            _Obj = obj;
        }

        public ReturnEntity(int code, string message)
        {
            _Code = code;
            _Message = message;
        }

        public ReturnEntity(int code, string message, T obj)
        {
            _Code = code;
            _Message = message;
            _Obj = obj;
        }

        public bool Success
        {
            get
            {
                if (_Code != 0)
                    return false;
                return true;
            }
        }        

        [DataMember]
        public int Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
            }
        }

        [DataMember]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        [DataMember]
        public T OBJ
        {
            get { return _Obj; }
            set { _Obj = value; }
        }
    }
}
