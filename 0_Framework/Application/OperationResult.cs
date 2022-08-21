using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool IsSuccedead { get; set; }
        public OperationResult()
        {
            IsSuccedead = false;
            Message = "";
        }
        public OperationResult Succesdead(string message = "عملیات با موفقیت انجام شد")
        {
            IsSuccedead = true;
            Message = message;
            return this;
        }
        public OperationResult Failed(string message)
        {
            IsSuccedead = false;
            Message = message;
            return this;
        }
    }
}
