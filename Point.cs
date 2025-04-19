using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
     class Point <T>
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }

        public Point()
        {
            Data = default(T);
            Next = null;
        }
        public Point (T info)
        {
            Data = info;
            Next = null;    
        }

        public override string ToString()
        {
            return Data?.ToString()??"null"; // проверка на нулевую ссылку
        }
    }
}
