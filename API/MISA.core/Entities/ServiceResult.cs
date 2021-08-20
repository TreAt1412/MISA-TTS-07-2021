using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        public bool isValid { get; set; } = true;

        public object Data { get; set; }

        public string Messenger { get; set; }
    }
}
