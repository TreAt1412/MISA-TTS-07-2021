using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService:IBaseService<Customer>
    {
        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);
    }
}
