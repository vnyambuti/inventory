using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS
{
    public interface IHelloService
    {
        string SayHello();
    }
    internal class LoadService:IHelloService
    {
        public string SayHello()
        {
            return "Hello, world!";
        }
    }
}
